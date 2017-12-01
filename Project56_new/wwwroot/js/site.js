// Write your JavaScript code.

function UpdateQtyOrder(ordline_id, new_qty) {

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
function SliderPagination(page_number, direction) {

            prev = page_number - 1; 
            next = page_number + 1;
            start = 4;

            if (direction == "next") {

                document.getElementById('col_' + page_number).style.visibility = "hidden";
                document.getElementById('col_' + (page_number - 1)).style.visibility = "hidden";
                document.getElementById('col_' + (page_number - 2)).style.visibility = "hidden";
                document.getElementById('col_' + (page_number - 3)).style.visibility = "hidden";


            } else {
                document.getElementById('col_' + page_number + 4).style.visibility = "visible";
                document.getElementById('col_' + (page_number + 5)).style.visibility = "visible";
                document.getElementById('col_' + (page_number + 6)).style.visibility = "visible";
                document.getElementById('col_' + (page_number + 7)).style.visibility = "visible";


}
 }

