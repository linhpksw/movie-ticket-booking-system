// Add this script either in a separate .js file or within a <script> tag
document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('.account-form');

    form.addEventListener('submit', function (e) {
        e.preventDefault(); // Prevent form submission until validation passes

        // Get form elements
        const email = document.getElementById('email1');
        const password = document.getElementById('pass1');
        const confirmPassword = document.getElementById('pass2');
        const termsCheckbox = document.getElementById('bal');

        // Clear previous error messages
        clearErrors();

        let isValid = true;

        // Email validation
        if (!isValidEmail(email.value)) {
            showError(email, 'Please enter a valid email address');
            isValid = false;
        }

        // Password validation
        if (!isValidPassword(password.value)) {
            showError(password, 'Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number');
            isValid = false;
        }

        // Confirm password validation
        if (password.value !== confirmPassword.value) {
            showError(confirmPassword, 'Passwords do not match');
            isValid = false;
        }

        // Terms checkbox validation
        if (!termsCheckbox.checked) {
            showError(termsCheckbox, 'You must agree to the terms and conditions');
            isValid = false;
        }

        // If all validations pass, submit the form
        if (isValid) {
            form.submit();
        }
    });

    // Email validation function
    function isValidEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    // Password validation function
    function isValidPassword(password) {
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
        return passwordRegex.test(password);
    }

    // Show error message
    function showError(input, message) {
        const formGroup = input.closest('.form-group');
        const errorElement = document.createElement('span');
        errorElement.className = 'error-message';
        errorElement.style.color = 'red';
        errorElement.style.fontSize = '0.8em';
        errorElement.textContent = message;
        formGroup.appendChild(errorElement);
    }

    // Clear previous error messages
    function clearErrors() {
        const errorMessages = document.querySelectorAll('.error-message');
        errorMessages.forEach(error => error.remove());
    }
});

// Optional CSS for better styling
const style = document.createElement('style');
style.textContent = `
    .form-group {
        position: relative;
        margin-bottom: 1.5rem;
    }
    .error-message {
        display: block;
        margin-top: 0.5rem;
    }
    .form-group input:invalid {
        border-color: #ff0000;
    }
`;
document.head.appendChild(style);