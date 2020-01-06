// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Wallet area

$("#walletSelect").change(function () {
    let wallet = {};
    let userInfo = JSON.parse(localStorage.getItem("userInfo"));
    for (i = 0; i < userInfo.wallets.length; i++) {
        if (userInfo.wallets[i].public_key == $("#walletSelect").val()) {
            wallet.public_key = userInfo.wallets[i].public_key;
            wallet.private_key = userInfo.wallets[i].private_key;
            break;
        }
    }
    $("#listWalletPublicKey").html("Public key: " + wallet.public_key);
    $("#listWalletPrivateKey").html("Private key: " + wallet.private_key);
    getWalletBalance(wallet.public_key).then(data => {
        $("#listWalletBalance").html("Balance: " + data + " ETH");
    })
});

function getWalletBalance(publicKey) {
    return axios.get('/Wallet/walletBalance/' + publicKey)
        .then((response) => {
            return response.data.balance / 1000000000000000000;
    }, (error) => {
            console.log(error);
            return -1;
    });
}
