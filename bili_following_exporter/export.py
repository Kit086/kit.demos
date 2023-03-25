import requests
import json

# 从 settings.json 文件中读取 Cookie 和 vmid
with open('settings.json', 'r', encoding='utf-8') as settings_file:
    settings = json.load(settings_file)
    cookie = settings['cookie']
    vmid = settings['vmid']

url = 'https://api.bilibili.com/x/relation/followings'

params = {
    'vmid': vmid,
    'pn': 0,
    'ps': 50,
    'order': 'desc',
    'order_type': 'attention',
    'jsonp': 'jsonp'
}

headers = {'Cookie': cookie}

results = []

while True:
    params['pn'] += 1
    response = requests.get(url, params=params, headers=headers)
    json_data = response.json()

    if 'data' not in json_data or 'list' not in json_data['data'] or len(json_data['data']['list']) == 0:
        break

    results.extend(json_data['data']['list'])

with open('output.json', 'w', encoding='utf-8') as output_file:
    json.dump(results, output_file, ensure_ascii=False, indent=4)
