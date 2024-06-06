window.addEventListener("load", function () {
    showContent();
});

function showContent() {
    const userId = localStorage.getItem("id");
    const basketData = JSON.parse(localStorage.getItem("Basket " + userId)) || [];
    const totalPrice = JSON.parse(localStorage.getItem("Price " + userId)) || 0;

    const template = document.querySelector("#temp-row");
    const tableBody = document.querySelector("#items tbody");
    const sumElement = document.querySelector(".sum");
    sumElement.textContent = totalPrice;

    const placeOrderButton = document.getElementById("placeOr");
    placeOrderButton.addEventListener("click", async function () {
        await placeOrder();
    });

    tableBody.innerHTML = '';

    basketData.forEach(itemData => {
        const clone = template.content.cloneNode(true);
        const itemRow = clone.querySelector("tr");

        itemRow.querySelector(".itemName").textContent = itemData.prod.name;
        itemRow.querySelector(".quantity").textContent = itemData.quantity;
        const imgElement = itemRow.querySelector(".image img");

        if (!imgElement) {
            const img = document.createElement("img");
            img.src = "../images/" + itemData.prod.image;
            img.style.width = "100px";
            itemRow.querySelector(".image").appendChild(img);
        } else {
            imgElement.src = "../images/" + itemData.prod.image;
            imgElement.style.width = "100px";
        }

        itemRow.querySelector(".availabilityColumn").textContent = "במלאי";
        itemRow.querySelector(".price").textContent = itemData.prod.price + " ₪";

        itemRow.querySelector(".delBtn").addEventListener("click", function () {
            deleteProd(itemData);
        });

        itemRow.querySelector(".plus").addEventListener("click", function () {
            addQuantity(itemData);
        });

        itemRow.querySelector(".minus").addEventListener("click", function () {
            decQuantity(itemData);
        });

        tableBody.appendChild(clone);
    });
}

function deleteProd(item) {
    const userId = localStorage.getItem("id");
    let basketData = JSON.parse(localStorage.getItem("Basket " + userId)) || [];
    let totalPrice = JSON.parse(localStorage.getItem("Price " + userId)) || 0;

    totalPrice -= item.prod.price * item.quantity;
    basketData = basketData.filter(i => i.prod.name !== item.prod.name);

    localStorage.setItem("Basket " + userId, JSON.stringify(basketData));
    localStorage.setItem("Price " + userId, totalPrice);

    showContent();
}

function addQuantity(item) {
    const userId = localStorage.getItem("id");
    let basketData = JSON.parse(localStorage.getItem("Basket " + userId)) || [];
    let totalPrice = JSON.parse(localStorage.getItem("Price " + userId)) || 0;

    basketData = basketData.map(i => {
        if (i.prod.name === item.prod.name) {
            i.quantity += 1;
            totalPrice += i.prod.price;
        }
        return i;
    });

    localStorage.setItem("Basket " + userId, JSON.stringify(basketData));
    localStorage.setItem("Price " + userId, totalPrice);

    showContent();
}

function decQuantity(item) {
    if (item.quantity === 1) {
        deleteProd(item);
        return;
    }

    const userId = localStorage.getItem("id");
    let basketData = JSON.parse(localStorage.getItem("Basket " + userId)) || [];
    let totalPrice = JSON.parse(localStorage.getItem("Price " + userId)) || 0;

    basketData = basketData.map(i => {
        if (i.prod.name === item.prod.name) {
            i.quantity -= 1;
            totalPrice -= i.prod.price;
        }
        return i;
    });

    localStorage.setItem("Basket " + userId, JSON.stringify(basketData));
    localStorage.setItem("Price " + userId, totalPrice);

    showContent();
}

async function placeOrder() {
    try {
        const userId = localStorage.getItem("id");
        let basketData = JSON.parse(localStorage.getItem("Basket " + userId));

        if (!basketData || basketData.length === 0) throw new Error("basket empty");

        const orderItems = basketData.map(item => ({
            ProductId: item.prod.productId,
            Quantity: item.quantity
        }));

        const order = {
            UserId: userId,
            Price: localStorage.getItem("Price " + userId),
            orderItems
        };

        const res = await fetch("api/Order", {
            method: "POST",
            body: JSON.stringify(order),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!res.ok) throw new Error("error");

        const data = await res.json();
        clearBasket();
        alert("order number " + data.orderId + " was placed successfully");
    } catch (err) {
        alert(err);
    }
}

function clearBasket() {
    const userId = localStorage.getItem("id");
    localStorage.setItem("Basket " + userId, JSON.stringify([]));
    localStorage.setItem("Price " + userId, 0);
    showContent();
}