"use strict"


// SideBar

let SideBarStatusStart = localStorage.getItem("statusSiteBar");
let statusSiteBar = false;
let header = document.querySelector("header");

if (SideBarStatusStart === "true") {
    statusSiteBar = true;
}
else {
    statusSiteBar = false
}

function changeStatusSideBar() {
    if (statusSiteBar) {
        sideBarWaveStudio.classList.remove("hiddenSideBar");
        sideBarWaveStudio.classList.add("visibleSideBar");
        header.classList.add("headerWithBorder");
        localStorage.setItem("statusSiteBar", true);
        statusSiteBar = false;
    }
    else {
        sideBarWaveStudio.classList.remove("visibleSideBar");
        header.classList.remove("headerWithBorder");
        sideBarWaveStudio.classList.add("hiddenSideBar");
        localStorage.setItem("statusSiteBar", false);
        statusSiteBar = true
    }

}

changeStatusSideBar();



btnSideBar.onclick = () => {
    changeStatusSideBar();
}

// AccountData

const myMenu = document.querySelector(".profileContextMenu");

profileMenubtn.addEventListener("click", (event) => {
    event.stopPropagation();
    event.preventDefault();
    myMenu.style.top = profileMenubtn.offsetTop + 35 + "px";
    myMenu.style.left = profileMenubtn.offsetLeft - 275 + "px";
    myMenu.classList.add("active");
});

myMenu.addEventListener("click", (event) => {
    event.stopPropagation();
});

bodyFromLayout.addEventListener("click", () => {
    myMenu.classList.remove("active");
});
