from flask import Flask, request, jsonify
import sys

app = Flask(__name__)

# Função do seu jogo em python
def escolher_palitos(restantes, max_remove=3):
    alvo = (max_remove + 1)
    resto = restantes % alvo
    if resto == 0:
        return 1
    else:
        return resto

@app.route('/api/ai-move', methods=['POST'])
def ai_move():
    data = request.json
    state = data.get('state', [])
    max_remove = 3
    # Exemplo: pegar total restante e calcular movimento
    restantes = sum(state)
    move = escolher_palitos(restantes, max_remove)
    return jsonify({'removed': move})

# Rotas para login e registro (simplificado)
@app.route('/api/register', methods=['POST'])
def register():
    data = request.json
    name = data.get('name')
    email = data.get('email')
    password = data.get('pass')
    # Aqui você deve conectar no banco e criar o usuário
    # Por enquanto só retorno simulado
    return jsonify({'user': {'name': name, 'email': email}}), 200

@app.route('/api/login', methods=['POST'])
def login():
    data = request.json
    email = data.get('email')
    password = data.get('pass')
    # Validar usuário no banco
    # Retorno simulado
    return jsonify({'user': {'name': 'Usuário Exemplo', 'email': email}}), 200

if __name__ == '__main__':
    app.run(debug=True)
