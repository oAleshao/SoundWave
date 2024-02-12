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
    allLanguages.style.display = "none";
});

bodyFromLayoutWaveStudio.addEventListener("click", () => {
    myMenu.classList.remove("active");
    allLanguages.style.display = "none";
    comboBoxGenres.style.display = "none";
    statusListGenres = false;
    comboBoxUsers.style.display = "none";
    statusListExecutors = false;
    SortFilterList.style.display = "none";
    statusListSort = false;
});

languages.addEventListener("click", (event) => {
    event.stopPropagation();
    allLanguages.style.display = "block";
});



let statusListGenres = false;
BtnOpenListGenres.addEventListener("click", (event) => {
    event.stopPropagation();
    if (statusListGenres === false) {
        comboBoxGenres.style.display = "block";
        statusListGenres = true;
    }
    else {
        comboBoxGenres.style.display = "none";
        statusListGenres = false;
    }
});

let statusListExecutors = false;
BtnOpenListExecutors.addEventListener("click", (event) => {
    event.stopPropagation();
    if (statusListExecutors === false) {
        comboBoxUsers.style.display = "block";
        statusListExecutors = true;
    }
    else {
        comboBoxUsers.style.display = "none";
        statusListExecutors = false;
    }
});

let statusListSort = false;
SortFilterBtn.addEventListener("click", (event) => {
    event.stopPropagation();
    if (statusListSort === false) {
        SortFilterList.style.display = "block";
        statusListSort = true;
    }
    else {
        SortFilterList.style.display = "none";
        statusListSort = false;
    }
});


delFilters.onclick = () => {
    filterGenres.value = "";
    SearchFieldInput.value = "";
    filterExecutors.value = "";
    btnSearch.click();
}

let SelectedGanresList = document.querySelectorAll(".SelectedGanres");
for (let i = 0; i < SelectedGanresList.length; i++) {
    SelectedGanresList[i].addEventListener("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        filterGenres.value = this.textContent;
        filterByGenreFormSend.value = this.textContent;
        bodyFromLayoutWaveStudio.click();
    });
}

let SelectedExecutorList = document.querySelectorAll(".SelectedExecutor");
for (let i = 0; i < SelectedExecutorList.length; i++) {
    SelectedExecutorList[i].addEventListener("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        filterExecutors.value = this.textContent;
        filterByExecutorFormSend.value = this.textContent;
        bodyFromLayoutWaveStudio.click();
    });
}
