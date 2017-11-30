// Write your JavaScript code.

function UpdateQtyOrder(ordline_id , new_qty) {

        $.ajax({
            type: "POST",
            data: {
                ordline_id: ordline_id,
                qty: new_qty
            },

            url: "/ShoppingCart/UpdateQtyInShoppingCart",
            async: true,
            success: function (response) {
                document.body.innerHTML = response;
            },
            error: function () {
                return "error";
            }
        });
 }

