# app.py

from flask import Flask, request, jsonify
from nin_ai import melhor_jogada

app = Flask(__name__)

@app.route('/ia-jogada', methods=['POST'])
def jogada_ia():
    data = request.json
    restantes = data.get("restantes")

    if restantes is None or not isinstance(restantes, int):
        return jsonify({"erro": "Número inválido"}), 400

    jogada = melhor_jogada(restantes)
    return jsonify({"jogada_ia": jogada})

if __name__ == '__main__':
    app.run(debug=True)
