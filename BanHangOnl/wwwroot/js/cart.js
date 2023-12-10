// Hàm để đặt giỏ hàng vào cookie
function setCartItems(cartItems) {
    setCookie("cart", JSON.stringify(cartItems), 7);
}

// Hàm để lấy giỏ hàng từ cookie
function getCartItems() {
    var storedCart = getCookie("cart");
    return JSON.parse(storedCart) || [];
}

// Hàm để thêm một hàng hóa vào giỏ hàng
function addToCart(item) {
    var cartItems = getCartItems();
    cartItems.push(item);
    setCartItems(cartItems);
}

// Hàm để xoá một hàng hóa khỏi giỏ hàng
function removeFromCart(item) {
    var cartItems = getCartItems();
    var index = cartItems.indexOf(item);
    if (index !== -1) {
        cartItems.splice(index, 1);
        setCartItems(cartItems);
    }
}

// Hàm để xoá toàn bộ giỏ hàng
function clearCart() {
    setCartItems([]);
}

// Hàm để đặt cookie
function setCookie(cookieName, cookieValue, expireDays) {
    var d = new Date();
    d.setTime(d.getTime() + (expireDays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cookieName + "=" + cookieValue + "; " + expires + "; path=/";
}

// Hàm để lấy giá trị của cookie theo tên
function getCookie(cookieName) {
    var name = cookieName + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(';');
    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i];
        while (cookie.charAt(0) == ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) == 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }
    return "";
}
function showNumberOfCart() {
    $('#checkout_items').text(getCartItems().length)
}

// 
if (!getCookie("cart")) {
    setCartItems([]);
}
showNumberOfCart();


$(document).on('click', '.add_to_cart_button a', function (event) {
    event.preventDefault();
    if (!getCookie("cart")) {
        setCartItems([]);
    }
    var a = $(this);
    var data = {
        idHh: a.data('idhh'),
        ten: a.data('ten'),
        sl: a.data('sl'),
        gia: a.data('gia'),
        mau: a.data('mau'),
        size: a.data('size'),
        url: a.data('url'),
    };
    addToCart(data);
    showNumberOfCart();
});
