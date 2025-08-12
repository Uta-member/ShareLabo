document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById('login-form-id');
    const submitButton = document.getElementById('loginButton');

    if (loginForm && submitButton) {
        loginForm.addEventListener('submit', () => {
            // ローディングUIの表示
            const spinner = document.getElementById('loginButtonSpinner');
            const authMessage = document.getElementById('loginButtonAuthMessage');
            const loginText = document.getElementById('loginButtonLoginText');

            if (spinner && authMessage && loginText) {
                spinner.style.display = 'inline-block';
                authMessage.style.display = 'inline';
                loginText.style.display = 'none';
            }

            loginForm.querySelectorAll(
                'input:not([type="hidden"]), textarea'
            ).forEach(el => el.readOnly = true);
            loginForm.querySelectorAll('button[type="submit"]').forEach(btn => btn.disabled = true);
        });
    }
});