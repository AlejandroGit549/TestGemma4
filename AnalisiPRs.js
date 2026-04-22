// analizaYComentaPR.js
const fs = require('fs');


const DIFF_FILE = 'pr-changes.diff';

if (!fs.existsSync(DIFF_FILE)) {
  console.error('❌ No existe pr-changes.diff');
  process.exit(1);
}
const diff = fs.readFileSync(DIFF_FILE, 'utf8');
// console.log('=== Contenido de pr-changes.diff ===\n', diff);

// 2) Construir prompt
const prompt = `
Eres un revisor experto de pull requests en proyectos empresariales.
Revisa este diff y responde en español, claro y conciso:

${diff}
`;

async function consultaOllama(pmpt) {
  console.log('=== Llamando HTTP a Ollama (192.168.10.126:11434) con timeout 15 minutos ===');

  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), 900000); // 15 minutos

  const _pmpt = pmpt
    .replace(/\r\n/g, '\\r\\n')
    .replace(/\r/g, '\\r')
    .replace(/\n/g, '\\n');

  const _body = JSON.stringify({
    model: 'gemma4',
    prompt: _pmpt,
    stream: false
  });

  try {
    const resp = await fetch('http://192.168.10.126:11434/api/generate', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        model: 'gemma4:31b',
        prompt: _pmpt,
        stream: false
      }),
      signal: controller.signal
    });

    if (!resp.ok) {
      throw new Error(`Error HTTP: ${resp.status} ${resp.statusText}`);
    }

    const data = await resp.json();
    console.log('=== Respuesta de Ollama ===\n', data.response.trim());
    return data.response.trim();

  } catch (err) {
    if (err.name === 'AbortError') {
      throw new Error('Timeout: Ollama no respondió en 15 minutos');
    }
    throw err;
  } finally {
    clearTimeout(timeoutId);
  }
}
async function ComentarPR(_commnet) {

  const owner = 'AlejandroGit549';
  const repo = 'TestGemma4';
  const pullNumber = '2'; // El número del PR
  const token = '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';

  const url = `https://api.github.com/repos/${owner}/${repo}/issues/${pullNumber}/comments`;
  const postComment = async () => {
    try {
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/vnd.github+json',
          'X-GitHub-Api-Version': '2026-03-10',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          body: _commnet
        })
      });

      if (!response.ok) {
        const error = await response.json();
        throw new Error(`Error ${response.status}: ${error.message}`);
      }

      const data = await response.json();
      console.log('Comentario creado con éxito:', data.html_url);
    } catch (error) {
      console.error('Error al crear el comentario:', error.message);
    }
  };
  postComment();
}

(async () => {
  console.log('=== Iniciando revisión IA del PR ===');
  try {
    const rev = await consultaOllama(prompt);
    await ComentarPR(rev);
  } catch (e) {
    console.error('❌ Error en el proceso:', e.message || e);
    process.exit(1);
  }
})();
