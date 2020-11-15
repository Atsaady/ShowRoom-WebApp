var allCheckboxes = document.querySelectorAll("input[type=checkbox]");
var allProducts = Array.from(document.querySelectorAll(".product"));
var checked = {};

getChecked("masks");
getChecked("hygiene");
getChecked("antiseptic");
getChecked("tech");

Array.prototype.forEach.call(allCheckboxes, function (el) {
    el.addEventListener("change", toggleCheckbox);
});

function toggleCheckbox(e) {
    getChecked(e.target.name);
    setVisibility();
}

function getChecked(name) {
    checked[name] = Array.from(
        document.querySelectorAll("input[name=" + name + "]:checked")
    ).map(function (el) {
        return el.value;
    });
}

function onCheckboxChanged(e, checkedName) {
    console.log(e);
    console.log(checkedName);
}

function setVisibility() {
    allProducts.map(function (el) {
        var masks = checked.masks.length
            ? _.intersection(Array.from(el.classList), checked.masks).length
            : true;
        var hygiene = checked.hygiene.length
            ? _.intersection(Array.from(el.classList), checked.hygiene).length
            : true;
        var antiseptic = checked.antiseptic.length
            ? _.intersection(Array.from(el.classList), checked.antiseptic).length
            : true;
        var tech = checked.tech.length
            ? _.intersection(Array.from(el.classList), checked.tech).length
            : true;
        if (masks && hygiene && antiseptic && tech) {
            el.style.display = "block";
        } else {
            el.style.display = "none";
        }
    });
}