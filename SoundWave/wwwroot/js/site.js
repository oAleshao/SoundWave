"use strict"



window.ondblclick = (event) => {
    event.preventDefault;
}

let SideBarStatusStart = localStorage.getItem("statusSiteBar");
let statusSiteBar = false;
let header = document.querySelector("header");
let headerSide = document.querySelector(".sidebarHeader");
let state = "play";
let idInterval = 0;
let tmpwidth = 1;
let videoPlayerJs = document.getElementById("videoPlayer");
let userChooseVideo = false;
let ranger = document.querySelector(".ranger");
let rangerFill = document.querySelector(".fill");
let currentTimeForToolTip = document.querySelector(".currentTime");
let seconds_timeToolTip = 0;
let repeat = "nonrepeat";
let tmpRep = localStorage.getItem("repeat")
let volumeValue = 1;
let volumeStatus = "Off";
let minutes = 0;
let seconds = 0;

//helper variables
let hrSideBar = null;
let btnAddNewPlaylist = null

// SideBar
if (SideBarStatusStart === "true") {
    statusSiteBar = true;
}
else {
    statusSiteBar = false
}

function changeStatusSideBar() {
    if (statusSiteBar) {
        sideBar.classList.remove("hiddenSideBar");
        sideBar.classList.add("visibleSideBar");
        header.classList.add("headerWithBorder");
        localStorage.setItem("statusSiteBar", true);
        AudioAndVideo.style.margin = "0px 0px 0px 20px";
        AudioAndVideo.style.width = "780px";
        statusSiteBar = false;

        hrSideBar = document.createElement("div");
        hrSideBar.classList.add("visibleHrSideBar");

        btnAddNewPlaylist = document.createElement("a");
        btnAddNewPlaylist.classList.add("visibleAddPlaylist");
        btnAddNewPlaylist.id = "btnAddNewPlaylist";
        let div_btnAddNewPlaylist = document.createElement("div")
        let img_div_btnAddNewPlaylist = document.createElement("img");
        img_div_btnAddNewPlaylist.src = "/img/plus.png";
        let h2_div_btnAddNewPlaylist = document.createElement("h2");
        h2_div_btnAddNewPlaylist.textContent = "Создать";

        div_btnAddNewPlaylist.appendChild(img_div_btnAddNewPlaylist);
        div_btnAddNewPlaylist.appendChild(h2_div_btnAddNewPlaylist);
        btnAddNewPlaylist.appendChild(div_btnAddNewPlaylist);

        sideBar.appendChild(hrSideBar);
        sideBar.appendChild(btnAddNewPlaylist);
    }
    else {
        sideBar.classList.remove("visibleSideBar");
        header.classList.remove("headerWithBorder");
        sideBar.classList.add("hiddenSideBar");
        localStorage.setItem("statusSiteBar", false);
        AudioAndVideo.style.margin = "0px 0px 0px 130px";
        AudioAndVideo.style.width = "820px";
        statusSiteBar = true

        if (hrSideBar != null) {
            sideBar.removeChild(hrSideBar);
            sideBar.removeChild(btnAddNewPlaylist);
        }
    }

}

changeStatusSideBar();



btnSideBar.onclick = () => {
    changeStatusSideBar();
}


/////////////// Player


// Volume


VolumeDiv.onmouseover = () => {
    volumeRange.classList.remove("hiddenInputVolume");
}

VolumeDiv.onmouseout = () => {
    volumeRange.classList.add("hiddenInputVolume");
}

volumeBtn.onclick = () => {
    if (volumeStatus === "Off") {
        volumeValue = volumeRange.value;
        volumeRange.value = 0;
        changeIconVolume();
        volumeStatus = "On";
    }
    else {
        volumeRange.value = volumeValue;
        changeIconVolume();
        volumeStatus = "Off";
    }
    if (userChooseVideo === true) {
        videoPlayerJs.volume = volumeRange.value / 100;
    }
    else {
        audioPlayer.volume = volumeRange.value / 100;
    }
}

volumeRange.volume = 1;

volumeRange.addEventListener("input", () => {
    if (userChooseVideo === true) {
        videoPlayerJs.volume = volumeRange.value / 100;
    }
    else {
        audioPlayer.volume = volumeRange.value / 100;
    }
    changeIconVolume();
});
volumeRange.addEventListener("change", () => {
    if (userChooseVideo === true) {
        videoPlayerJs.volume = volumeRange.value / 100;
    }
    else {
        audioPlayer.volume = volumeRange.value / 100;
    }
    changeIconVolume();
});

function changeIconVolume() {
    if (volumeRange.value <= 3) {
        volumeIcon.src = "/img/volumeOff.png";
    }
    else if (volumeRange.value <= 10) {
        volumeIcon.src = "/img/volumeTen.png";
    }
    else if (volumeRange.value < 80) {
        volumeIcon.src = "/img/volumeMidle.png";
    }
    else {
        volumeIcon.src = "/img/volumeOn.png";
    }
}



// play/pause

if (videoPlayerJs != null) {
    userChooseVideo = true;
}


//  Repeat

;
if (tmpRep != null)
    repeat = tmpRep;


function repeatBtnClick() {
    if (repeat === "nonrepeat") {
        repeat = "repeat";
        RepeatBtnImg.src = "/img/repeat-once.png";
        if (userChooseVideo === true) {
            videoPlayerJs.loop = true;
        }
        else {
            audioPlayer.loop = true;
        }

        localStorage.setItem("repeat", "nonrepeat");
    }
    else {
        repeat = "nonrepeat";
        RepeatBtnImg.src = "/img/repeat.png";
        if (userChooseVideo === true) {
            videoPlayerJs.loop = false;
        }
        else {
            audioPlayer.loop = false;
        }
        localStorage.setItem("repeat", "repeat");
    }
}

RepeatBtn.onclick = () => {
    repeatBtnClick();
}
repeatBtnClick();



imgOrVideo.onclick = () => {
    playPause();
}

playbtn.onclick = () => {
    playPause();
}



function playPause() {
    if (state === "play") {
        playbtnImg.src = "/img/pause.png";
        state = "pause";
        if (userChooseVideo === true) {
            videoPlayerJs.play();
        }
        else {
            audioPlayer.play();
        }
        setTimeout(() => {
            seconds_timeToolTip = Math.floor(audioPlayer.duration);
            ranger.min = 0;
            ranger.max = seconds_timeToolTip;
            console.log(ranger.max + " | " + seconds_timeToolTip);
        }, 200);
        LaunchProgressBar();

    }
    else {
        playbtnImg.src = "/img/play.png";
        state = "play"
        if (userChooseVideo === true) {
            videoPlayerJs.pause();
        }
        else {
            audioPlayer.pause();
        }
        clearInterval(idInterval);
    }
}

previousBtn.onclick = () => {
    localStorage.setItem("wasClickPrevOrNext", true);
}


nextBtn.onclick = () => {
    localStorage.setItem("wasClickPrevOrNext", true);
}

function playSong() {
    if (localStorage.getItem("wasClickPrevOrNext") === "true") {
        localStorage.setItem("wasClickPrevOrNext", false);
        setDurationTime();
    }
}

function setDurationTime() {
    playbtnImg.src = "/img/pause.png";
    state = "play";
    clearInterval(idInterval);
    tmpwidth = 1;
    let event = new Event("click");
    playbtn.dispatchEvent(event);
}


function LaunchProgressBar() {

    //for (let i = 0; i < ranger.max; i++) {
    //    const span = document.createElement("span")
    //    minutes = Math.floor(i / 60);
    //    seconds = Math.floor(i - minutes * 60);
    //    if (seconds >= 10)
    //        span.textContent = minutes + ":" + seconds;
    //    else
    //        span.textContent = minutes + ":0" + seconds;
    //    span.classList.add("currentTime");
    //    span.classList.add("show");
    //    spansCurrentTime.appendChild(span);
    //}

    currentTimeForToolTip = document.querySelector(".currentTime");

    idInterval = setInterval(() => {
        console.log(tmpwidth);
        ranger.value = tmpwidth;
        rangerFill.style.width = (100 / ranger.max) * ranger.value + "%";

        if (userChooseVideo === true) {
            minutes = Math.floor(videoPlayerJs.duration / 60);
            seconds = Math.floor(videoPlayerJs.duration - minutes * 60);
            currentSongDuration.textContent = minutes + ":" + seconds;
            durationTime.textContent = minutes + ":" + seconds;
        }

        if (userChooseVideo === true) {
            minutes = Math.floor(videoPlayerJs.currentTime / 60);
            seconds = Math.floor(videoPlayerJs.currentTime - minutes * 60);
        }
        else {
            minutes = Math.floor(audioPlayer.currentTime / 60);
            seconds = Math.floor(audioPlayer.currentTime - minutes * 60);
        }
        tmpwidth += 1;

        if (tmpwidth >= ranger.max) {
            if (repeat === "repeat") {
                localStorage.setItem("wasClickPrevOrNext", true);
                playSong();
            }
            else {
                nextBtn.dispatchEvent(new Event("click"));
                formnextBtn.submit();
                clearInterval(idInterval);
            }
        }
        if (seconds >= 10)
            currentTime.textContent = minutes + ":" + seconds;
        else
            currentTime.textContent = minutes + ":0" + seconds;
    }, 1000)
}

let tmp = document.getElementsByClassName("songV");

for (let item of tmp) {
    item.onclick = () => {
        localStorage.setItem("wasClickPrevOrNext", true);
    }
}

playSong();


// ProgressBar
ranger.value = 0;
rangerFill.style.width = (ranger.value) + "%";

ranger.onmouseover = (event) => {
    ranger.classList.remove("hiddenThumb");
    currentTimeForToolTip.classList.remove("show");
}

ranger.onmousemove = (event) => {
    if (event.clientX >= window.innerWidth-20)
        currentTimeForToolTip.style.left = (window.innerWidth - 50) + "px";
    else if (event.clientX <= 20) {
        currentTimeForToolTip.style.left = 10 + "px";
    }
    else
        currentTimeForToolTip.style.left = (event.clientX - 25) + "px";
}

ranger.onmouseout = () => {
    ranger.classList.add("hiddenThumb");
    currentTimeForToolTip.classList.add("show");
}


ranger.oninput = () => {
    audioPlayer.volume = 0;
    changeFillRanger();

}
ranger.onchange = () => {
    audioPlayer.currentTime = ranger.value;
    audioPlayer.volume = 1;
    tmpwidth = Number(ranger.value);
    minutes = Math.floor(ranger.value / 60);
    seconds = Math.floor(ranger.value - minutes * 60);
    if (seconds >= 10)
        currentTime.textContent = minutes + ":" + seconds;
    else
        currentTime.textContent = minutes + ":0" + seconds;
}


function changeFillRanger() {
    rangerFill.style.width = (100 / ranger.max) * ranger.value + "%";
    minutes = Math.floor(ranger.value / 60);
    seconds = Math.floor(ranger.value - minutes * 60);
    if (seconds >= 10)
        currentTimeForToolTip.textContent = minutes + ":" + seconds;
    else
        currentTimeForToolTip.textContent = minutes + ":0" + seconds;
    
}




// AccountData

const mymenu = document.querySelector(".profileContextMenu");
const btnmymenu = document.getElementById("profileMenubtn");

if (btnmymenu !== null) {
    btnmymenu.addEventListener("click", (event) => {
        event.stopPropagation()
        event.preventDefault
        mymenu.style.top = profileMenubtn.offsetTop + 35 + "px";
        mymenu.style.left = profileMenubtn.offsetLeft - 275 + "px";
        mymenu.classList.add("active");
    });

    mymenu.addEventListener("click", (event) => {
        event.stopPropagation();
    });

    bodyFromLayout.addEventListener("click", () => {
        mymenu.classList.remove("active");
    });
}



