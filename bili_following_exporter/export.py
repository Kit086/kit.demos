import requests
import json

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

headers = { 'cookie': cookie }

data = []

while True:
    params['pn'] += 1
    response = requests.get(url, params=params, headers=headers)
    json_data = response.json()

    if 'data' not in json_data or 'list' not in json_data['data'] or len(json_data['data']['list']) == 0:
        break

    data.extend(json_data['data']['list'])

unames = [item['uname'] for item in data]

with open('unames.txt', 'w', encoding='utf-8') as output_file:
    for uname in unames:
        output_file.write(uname + '\n')
