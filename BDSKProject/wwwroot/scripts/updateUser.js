window.addEventListener("load", async function () {
    try {
        const id = JSON.parse(localStorage.getItem("id"));
        const res = await fetch(`api/User/${id}`);
        if (!res.ok) throw new Error("user not found");

        const data = await res.json();

        const elements = {
            username: document.getElementById("txtUsernameUp"),
            password: document.getElementById("password"),
            firstName: document.getElementById("txtFirstNameUp"),
            lastName: document.getElementById("txtLastNameUp"),
            email: document.getElementById("txtEmail")
        };

        elements.username.value = data.userName.trim();
        elements.password.value = data.password.trim();
        elements.firstName.value = (data.firstName || '').trim();
        elements.lastName.value = (data.lastName || '').trim();
        elements.email.value = (data.email || '').trim();

        await checkPassword();
    } catch (e) {
        alert(e);
    }
});

const checkPassword = async () => {
    try {
        const password = document.getElementById("password").value;
        const passColor = document.getElementById("passwordColor");
        const res = await fetch("api/User/password", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });

        if (!res.ok) throw new Error("incorrect username or password");

        const data = await res.json();
        const colorMap = ["red", "orange", "green"];
        passColor.style.setProperty("background-color", colorMap[Math.min(data, 2)]);
    } catch (e) {
        alert(e);
    }
};

const update = async () => {
    try {
        const id = JSON.parse(localStorage.getItem("id"));
        const elements = {
            username: document.getElementById("txtUsernameUp"),
            password: document.getElementById("password"),
            firstName: document.getElementById("txtFirstNameUp"),
            lastName: document.getElementById("txtLastNameUp"),
            email: document.getElementById("txtEmail"),
            passColor: document.getElementById("passwordColor")
        };

        const user = {
            username: elements.username.value.trim(),
            password: elements.password.value.trim(),
            firstName: elements.firstName.value.trim(),
            lastName: elements.lastName.value.trim(),
            email: elements.email.value.trim()
        };

        if (!user.username || !user.password) throw new Error("Username and password are required");
        if (elements.passColor.style.getPropertyValue("background-color") != "green") throw new Error("Password strength is not sufficient");
        if (user.email && !validateEmail(user.email)) throw new Error("Invalid email format");

        const res = await fetch(`api/User/${id}`, {
            method: "PUT",
            body: JSON.stringify(user),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (res.ok) {
            alert("updated");
            window.location.replace("Products.html");
        } else {
            alert("error");
        }
    } catch (err) {
        alert(err);
        console.log(err);
    }
};

function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}