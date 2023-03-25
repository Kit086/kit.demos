import json

with open('output.json', 'r', encoding='utf-8') as input_file:
    data = json.load(input_file)

unames = [item['uname'] for item in data]

with open('unames.txt', 'w', encoding='utf-8') as output_file:
    for uname in unames:
        output_file.write(uname + '\n')