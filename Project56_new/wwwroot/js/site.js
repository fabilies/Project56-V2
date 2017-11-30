// Write your JavaScript code.
function SliderPagination(page_number , direction) {

    prev = page_number - 1;
    next = page_number + 1;
    start = 4;

     if (direction  ==  "next") {

         document.getElementById('col_' + page_number).style.visibility = "hidden";
         document.getElementById('col_' + (page_number - 1)).style.visibility = "hidden";
         document.getElementById('col_' + (page_number - 2)).style.visibility = "hidden";
         document.getElementById('col_' + (page_number - 3)).style.visibility = "hidden";


    } else {
         document.getElementById('col_' + page_number + 4 ).style.visibility = "visible";
         document.getElementById('col_' + (page_number + 5)).style.visibility = "visible";
         document.getElementById('col_' + (page_number + 6)).style.visibility = "visible";
         document.getElementById('col_' + (page_number + 7)).style.visibility = "visible";

        
    }
}