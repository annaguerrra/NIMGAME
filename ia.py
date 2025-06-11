import sys

def escolher_palitos(restantes, max_remove=3):
    # Estratégia: deixe múltiplo de (max_remove+1)
    alvo = (max_remove + 1)
    resto = restantes % alvo
    if resto == 0:
        # sem posição vantajosa: remove 1
        return 1
    else:
        # tire o suficiente para chegar ao múltiplo
        return resto

if __name__ == '__main__':
    # Recebe número de palitos restantes via argumento
    restantes = int(sys.argv[1])
    palitos = escolher_palitos(restantes)
    print(palitos)