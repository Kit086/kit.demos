from flask import Flask, render_template, request, jsonify
from pyllamacpp.model import Model

app = Flask(__name__)

model = Model(ggml_model='./models/gpt4all-converted.bin', n_ctx=512)

@app.route('/')
def index():
    return render_template('index.html')

@app.post('/generate')
def generate_text():
    input_text = request.form['input_text']
    generated_text = model.generate(input_text, n_predict=55)
    return jsonify({'response': generated_text})