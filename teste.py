# teste.py

import requests

resposta = requests.post("http://localhost:5000/ia-jogada", json={"restantes": 10})
print("Resposta da IA:", resposta.json())


