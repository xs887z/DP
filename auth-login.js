const handleLogin = async (email, password) => {
  try {
    const response = await axios.post('/api/account/login', { email, password });
    localStorage.setItem('token', response.data.token);
    updateAuthUI(true);
  } catch (error) {
    showError('Неверные учетные данные');
  }
};


document.getElementById('login-form')?.addEventListener('submit', (e) => {
  e.preventDefault();
  const email = document.getElementById('login-email').value;
  const password = document.getElementById('login-password').value;
  handleLogin(email, password);
});