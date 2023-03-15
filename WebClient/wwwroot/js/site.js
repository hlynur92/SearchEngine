//let me = this;

/*me.searchTerms = ko.observable();
me.hits = ko.observable();
me.results = ko.observableArray();
me.timeUsed = ko.observable();

me.search = function () {
    $.ajax({
        url: "http://loadbalancer-1/Load/search?terms=" + me.searchTerms() + "&numberOfResults=10",
        success: function (data) {
            me.hits(data.documents.length);
            me.timeUsed(data.elapsedMilliseconds);
            me.results.removeAll();
            data.documents.foreach(function (hit) {
                me.results.push(hit);
            });
        }
    });
};*/

/*
$(document).ready(function () {
    $("form-group.strategy").change(function () {
        var selectedStrategy = $(this).children("option:selected").val();
        alert("You have selected the strategy - " + selectedStrategy);
    });
});  
*/
var submitOption = document.getElementById("submit-option");

var submitSearch = document.getElementById("submit-search");
var showSearch = document.getElementById("display-search");

// Attach function to handle button click
submitSearch.addEventListener("click", handleSearch);

function handleSearch(event) {
    event.preventDefault();
    var inputBox = document.forms['search-form'].search;
    showSearch.innerText = `You have searched for: ${inputBox.value}`

    //HTTP Request
    fetch(window.location.origin + "/Home/Search?terms=" + inputBox.value + "&numberOfResults=10", {
        method: "GET",
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
        .then((response) => {
            console.log(response);
            response.json().then((mjson) => {
                console.log('my json', mjson);
            })
            
        })
        
}

// Function to display selected value on screen
function handleSubmit(event) {
    event.preventDefault();
    var selectedOption = document.forms['strategy-form'].strategy;

    //HTTP Request
    fetch(window.location.origin + "/Home/SetActiveStrategy", {
        method: "POST",
        body: JSON.stringify({
            strategy: selectedOption.value,
        }),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
        .then((response) => response.json())
        .then((json) => console.log(json));
}

//ko.applyBindings(new ViewModel());