@echo off

echo 创建 python 虚拟环境...
py -m venv .

echo 启用 python 虚拟环境...
call .\Scripts\activate

echo 安装依赖...
py -m pip install -r .\requirements.txt

echo 安装 Playwright...
playwright install firefox

echo 导出关注列表...
py .\export.py

echo 从关注列表中获取用户名列表...
py .\get_unames.py

echo 执行关注...
pytest .\follow.py

deactivate

echo 执行完成！

pause
