import time

from playwright.sync_api import Playwright, sync_playwright, expect

with open('unames.txt', 'r', encoding='utf-8') as input_file:
    unames = [line.strip() for line in input_file.readlines() if line.strip()]

def run(playwright: Playwright, unames) -> None:
    browser = playwright.firefox.launch(headless=False)
    context = browser.new_context()
    page = context.new_page()
    page.goto("https://search.bilibili.com/upuser")
    page.get_by_text("立即登录").click()
    time.sleep(20)

    already_followed_unames = [] # 早已关注的 unames
    followed_unames = [] # 关注成功的 unames
    failed_unames = [] # 关注失败的 unames
    notfound_unames = [] # 未找到的 unames
    uncertain_unames = [] # 关注存疑的 unames

    for uname in unames:
        page.get_by_placeholder("输入关键字搜索").click()
        page.get_by_placeholder("输入关键字搜索").fill(uname)
        page.get_by_placeholder("输入关键字搜索").press("Enter")
        time.sleep(2)  # 等待页面加载

        # follow_buttons = page.get_by_role("button", name="+ 关注")
        follow_buttons = page.query_selector_all("button[class='vui_button vui_button--blue']")

        if follow_buttons:
            follow_buttons[0].click() # 点击关注
            time.sleep(1)  # 等待按钮文本更新

            followed_buttons = page.query_selector_all("button[class='vui_button vui_button--grey']") # 已关注按钮

            if len(followed_buttons) == 1:
                # 如果有已关注按钮，且只有一个，我们认为关注成功（实际上不一定成功，因为我们已关注的未必是第一个搜索结果）
                followed_unames.append(uname)
            elif len(followed_buttons) > 1:
                # 如果有已关注按钮，且有多个，说明关注存疑，可能关注了其它账号
                uncertain_unames.append(uname)
            else:
                # 如果没有已关注按钮，说明关注失败
                failed_unames.append(uname)
        else:
            followed_buttons = page.query_selector_all("button[class='vui_button vui_button--grey']")
            # 如果没有关注按钮，且没有已关注按钮，说明未找到
            if not followed_buttons:
                notfound_unames.append(uname)
            else:
                # 如果没有关注按钮，但有已关注按钮，说明早已关注
                already_followed_unames.append(uname)

        time.sleep(1)

    # 将失败的 unames 写入 txt 文件
    if failed_unames:
        with open('failed_unames.txt', 'w', encoding='utf-8') as output_file:
            for failed_uname in failed_unames:
                output_file.write(f'{failed_uname}\n')

    # 将未找到的 unames 写入 txt 文件
    if notfound_unames:
        with open('notfound_unames.txt', 'w', encoding='utf-8') as output_file:
            for notfound_uname in notfound_unames:
                output_file.write(f'{notfound_uname}\n')

    # 将关注成功的 unames 写入 txt 文件
    if followed_unames:
        with open('followed_unames.txt', 'w', encoding='utf-8') as output_file:
            for followed_uname in followed_unames:
                output_file.write(f'{followed_uname}\n')

    # 将关注存疑的 unames 写入 txt 文件
    if uncertain_unames:
        with open('uncertain_unames.txt', 'w', encoding='utf-8') as output_file:
            for uncertain_uname in uncertain_unames:
                output_file.write(f'{uncertain_uname}\n')

    # 将早已关注的 unames 写入 txt 文件
    if already_followed_unames:
        with open('already_followed_unames.txt', 'w', encoding='utf-8') as output_file:
            for already_followed_uname in already_followed_unames:
                output_file.write(f'{already_followed_uname}\n')

    # ---------------------
    context.close()
    browser.close()

with sync_playwright() as playwright:
    run(playwright, unames)
