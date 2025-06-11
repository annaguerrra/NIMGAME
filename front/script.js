let user = null, isFirstTime = true;
let gameMode = null, currentDifficulty = 'easy';
let boardState = [], currentPlayer = 'player1';
let scores = { player1: 0, player2: 0 };
let friend = null;

// 🎯 Funções UI
function hideAll() { document.querySelectorAll('section').forEach(s => s.classList.add('hidden')); }
function updateUserUI() {
  const userInfo = document.getElementById('user-info');
  if (user) {
    userInfo.classList.remove('hidden');
    userInfo.innerHTML = `<img src="user-icon.png" alt="Usuário" style="width:24px;vertical-align:middle;" /> ${user.name}`;
  } else {
    userInfo.classList.add('hidden');
  }
}
function toggleUserMenu() { document.getElementById('user-menu').classList.toggle('hidden'); }

function showRegister() { hideAll(); document.getElementById('register').classList.remove('hidden'); }
function showLogin() { hideAll(); document.getElementById('login').classList.remove('hidden'); }
function showInstructions() { hideAll(); document.getElementById('instructions').classList.remove('hidden'); }
function showDifficulties() { hideAll(); document.getElementById('difficulty').classList.remove('hidden'); }
function showGame() { hideAll(); document.getElementById('game').classList.remove('hidden'); }

// 🧾 Autenticação
window.register = async function () {
  const name = document.getElementById('reg-name').value.trim();
  const email = document.getElementById('reg-email').value.trim();
  const pass = document.getElementById('reg-pass').value.trim();
  if (!name || !email || !pass) { return alert("Preencha todos os campos"); }
  try {
    const res = await fetch('/api/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name, email, pass })
    });
    const json = await res.json();
    if (res.ok) {
      user = json.user;
      updateUserUI();
      showInstructions();
    } else {
      alert(json.error || "Erro no cadastro");
    }
  } catch (e) {
    alert("Erro de conexão: " + e.message);
  }
};

window.login = async function () {
  const email = document.getElementById('log-email').value.trim();
  const pass = document.getElementById('log-pass').value.trim();
  if (!email || !pass) { return alert("Preencha email e senha"); }
  try {
    const res = await fetch('/api/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, pass })
    });
    const json = await res.json();
    if (res.ok) {
      user = json.user;
      updateUserUI();
      showInstructions();
    } else {
      alert(json.error || "Erro no login");
    }
  } catch (e) {
    alert("Erro de conexão: " + e.message);
  }
};

window.logout = function () {
  user = null;
  updateUserUI();
  hideAll();
  document.getElementById('auth').classList.remove('hidden');
};

window.changeName = function () {
  if (!user) return alert("Faça login primeiro");
  const novo = prompt("Novo nome:");
  if (novo) {
    user.name = novo.trim();
    updateUserUI();
  }
};

// 🎮 Jogo
window.startGame = function (diff, mode) {
  currentDifficulty = diff;
  gameMode = mode;
  scores = { player1: 0, player2: 0 };
  currentPlayer = 'player1';
  setupBoard();
  updateScores();
  showGame();
};

function updateScores() {
  // Você pode criar uma área para mostrar os scores se quiser.
  // Exemplo simples no console:
  console.log(`Placar: ${user.name} - ${scores.player1}, Abelha - ${scores.player2}`);
}

function setupBoard() {
  boardState = [];
  const board = document.getElementById('board');
  board.innerHTML = '';
  let linhas = currentDifficulty === 'easy' ? 5 : currentDifficulty === 'medium' ? 6 : 7;
  for (let i = 1; i <= linhas; i++) {
    const line = document.createElement('div');
    line.className = 'line';
    boardState[i - 1] = [];
    for (let j = 0; j < i; j++) {
      const pal = document.createElement('div');
      pal.className = 'palito';
      pal.onclick = () => {
        pal.classList.toggle('selected');
        boardState[i - 1][j] = !boardState[i - 1][j];
      };
      line.appendChild(pal);
      boardState[i - 1][j] = false;
    }
    board.appendChild(line);
  }
}

window.confirmSelection = async function () {
  const selecionados = document.querySelectorAll('.palito.selected');
  if (selecionados.length === 0) return alert('Selecione pelo menos uma bolinha');

  // Atualizar UI e estado
  selecionados.forEach(pal => {
    pal.classList.remove('selected');
    pal.classList.add('removido');
  });

  scores[currentPlayer] += selecionados.length;
  updateScores();

  // Verificar fim do jogo
  if (checkGameOver()) return;

  currentPlayer = currentPlayer === 'player1' ? 'player2' : 'player1';

  if (gameMode === 'bee' && currentPlayer === 'player2') {
    // Manda estado para o backend pedir jogada da IA
    const state = boardState.map(line => line.filter(v => !v).length);
    try {
      const res = await fetch('/api/ai-move', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ state, difficulty: currentDifficulty }),
      });
      const json = await res.json();
      if (res.ok) {
        // Atualiza board conforme resposta da IA
        // Aqui você atualiza a interface conforme o movimento da IA
        alert('Jogada da IA: removeu ' + json.removed);
        currentPlayer = 'player1';
      } else {
        alert('Erro na jogada da IA');
      }
    } catch (e) {
      alert('Erro na comunicação com IA: ' + e.message);
    }
  }
};

function checkGameOver() {
  // Simplificação para seu modelo: se todas linhas estiverem vazias, acaba o jogo
  const totalRestantes = boardState.reduce((acc, line) => acc + line.filter(v => !v).length, 0);
  if (totalRestantes === 0) {
    alert('Fim de jogo!');
    showDifficulties();
    return true;
  }
  return false;
}

window.restartGame = function () {
  startGame(currentDifficulty, gameMode);
};

window.exitToMenu = function () {
  hideAll();
  document.getElementById('auth').classList.remove('hidden');
};

// Deixe essas funções globais:
window.showRegister = showRegister;
window.showLogin = showLogin;
window.showInstructions = showInstructions;
window.showDifficulties = showDifficulties;
window.showGame = showGame;
window.toggleUserMenu = toggleUserMenu;
