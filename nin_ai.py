# nin_ai.py

def melhor_jogada(restantes: int) -> int:
    """
    Decide a jogada da IA com base na estratégia ótima do jogo Nin.
    IA tenta deixar múltiplos de 4 para o oponente.
    """
    if restantes <= 0:
        return 0
    for jogada in [1, 2, 3]:
        if (restantes - jogada) % 4 == 0:
            return jogada
    return 1  # Se não for possível deixar múltiplo de 4, tira 1
