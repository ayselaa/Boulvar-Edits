let buildings = document.getElementsByClassName("building-checkbox");
let floors = document.getElementsByClassName("floor-checkbox");
let rooms = document.getElementsByClassName("room-checkbox");
let features = document.getElementsByClassName("feature-checkbox");

let buildingsArr = [];
let floorsArr = [];
let roomsArr = [];
let featureArr = [];
let page = 1;

let minvaluerange;
let maxvaluerange;


//let minvaluerange = document.getElementById('rangeInput1').value;
//let maxvaluerange = document.getElementById('rangeInput2').value;

document.querySelector('.js-send-sliders').addEventListener('click', async function () {
    // İki range input'un değerlerini alalım
    debugger;
    let rangeInput1Value = document.getElementById('rangeInput1').value;
    let rangeInput2Value = document.getElementById('rangeInput2').value;

    // Değerleri console'a yazdıralım
    //console.log('Range Input 1 Değeri:', rangeInput1Value);
    minvaluerange = rangeInput1Value;
    //console.log('Range Input 2 Değeri:', rangeInput2Value);
    maxvaluerange = rangeInput2Value;


    await updateData();
});


Array.from(document.getElementsByClassName("resetFilter")).forEach((reset) => {
    reset.addEventListener("click", () => {

        Array.from(buildings).forEach(e => {
            e.checked = false;
        });

        Array.from(floors).forEach(e => {
            e.checked = false;
        });

        Array.from(rooms).forEach(e => {
            e.checked = false;
        });

        buildingsArr = [];
        floorsArr = [];
        roomsArr = [];

        updateData();
    });
});

/*updateData();*/



async function Test() {
     //let response = await axios.get(`/Apartment/GetApartmentData?buildingsArr=${buildingsArr}&floorArr=${floorsArr}&roomArr=${roomsArr}&featureArr=${featureArr}&page=${page}`);
     //console.log(response.data);

    //await axios.get(`/Apartment/GetApartmentData?buildingsArr=${buildingsArr}&floorArr=${floorsArr}&roomArr=${roomsArr}&featureArr=${featureArr}&page=${page}`)
    //     .then(function (response) {
    //         // handle success
    //         console.log(response);
    //     })

   fetch(`/Apartment/GetApartmentData?buildingsArr=${buildingsArr}&floorArr=${floorsArr}&roomArr=${roomsArr}&featureArr=${featureArr}&page=${page}`)
    .then(response => response.json())
        .then(data => console.log(data))
        .catch(err => console.log(err))
    /* const movies = await response.json();*/
    //console.log(response);
}

Test();

var pageLinks = document.querySelectorAll('a.page-link');
// Add a click event listener to each anchor tag
pageLinks.forEach(function (link) {
    link.addEventListener('click', function () {
        // Get and log the id attribute of the clicked anchor tag
        var clickedLinkId = link.id;
        page = clickedLinkId;
        updateData().then(function () {

        });

        document.querySelectorAll('li').forEach(function (li) {
            li.classList.remove('disabled');
        });

        // Add the 'disabled' attribute to the li element with id='1'
        var liWithId = document.getElementById(page.toString())
        if (liWithId) {
            liWithId.classList.add('disabled');
        }
    });
});

Array.from(buildings).forEach((buildData) => {
    buildData.addEventListener("click", async (e) => {
        if (buildingsArr.includes(e.target.getAttribute("building-data"))) {
            let index = buildingsArr.indexOf(e.target.getAttribute("building-data"));
            if (index !== -1) {
                buildingsArr.splice(index, 1);
            }
        } else {
            buildingsArr.push(e.target.getAttribute("building-data"));
        }
        page = 1;
        await updateData();
    });
});



Array.from(floors).forEach((floorData) => {
    floorData.addEventListener("click", async (e) => {
        if (floorsArr.includes(e.target.getAttribute("floor-data"))) {
            let index = floorsArr.indexOf(e.target.getAttribute("floor-data"));
            if (index !== -1) {
                floorsArr.splice(index, 1);
            }
        } else {
            floorsArr.push(e.target.getAttribute("floor-data"));
        }
        page = 1;
        await updateData();
    });
});

Array.from(rooms).forEach((roomData) => {
    roomData.addEventListener("click", async (e) => {
        if (roomsArr.includes(e.target.getAttribute("room-data"))) {
            let index = roomsArr.indexOf(e.target.getAttribute("room-data"));
            if (index !== -1) {
                roomsArr.splice(index, 1);
            }
        } else {
            roomsArr.push(e.target.getAttribute("room-data"));
        }
        page = 1;
        await updateData();
    });
});

Array.from(features).forEach((featureData) => {
    featureData.addEventListener("click", async (e) => {
        if (featureArr.includes(e.target.getAttribute("feature-data"))) {
            let index = featureArr.indexOf(e.target.getAttribute("feature-data"));
            if (index !== -1) {
                featureArr.splice(index, 1);
            }
        } else {
            featureArr.push(e.target.getAttribute("feature-data"));
        }
        page = 1;
        await updateData();
    });
});



async function updateData() {
    debugger
    let response = await axios.get(`/Apartment/GetApartmentData?buildingsArr=${buildingsArr}&floorArr=${floorsArr}&roomArr=${roomsArr}&featureArr=${featureArr}&page=${page}`);
    console.log(response.data)
    var dataField = document.getElementById("dataField");
    dataField.innerHTML = "";
    // Assuming 'response.data' is an array of objects

    console.log(response.data.apartments)
    console.log(response.data.apartments.length)
    Array.from(document.getElementsByClassName("ApartmentsFound")).forEach(p => {
        p.innerHTML = response.data.total
    })
    response.data.apartments.forEach(function (obj) {


        //foreach (var feat in item.ApartmentFeatures)
        //            {
        //                foreach (var items in feat?.Feature?.FeatureTranslates)
        //                {
        //
        //                }

        //            }
        var tempDiv = document.createElement("div");

        let apartmentFeatures = Array.from(obj.apartmentFeatures).map(item => {
            return Array.from(item.feature.featureTranslates).map(item2 => {
                return `
                                        <div class="img-fet">
                                            <img src="/featurelogo/${item.feature.logo}" alt="img">
                                            <div class="tooltip-sea">${item2.name}</div>
                                        </div>`;
            }).join('');
        }).join('');


        console.log(obj)

        let apartmentCard = `
                                                                                                <a class="tb-item" href="/Apartment/ApartmentDetail?id=${obj.id}">
                                                                                                    <div class="table-item-info">
                                                                                                        <div class="table-item-img">
                                                                                                            <img src="/apartmentplangallery/${obj.apartmentPlan}" alt="img" >
                                                                                                        </div>
                                                                                                        <div class="table-item-floor">
                                                                                                            <p>${obj.sectionName}</p>
                                                                                                            <span>${obj.floor} Floor</span>

                                                                                                        </div>
                                                                                                        <div class="table-item-rooms">
                                                                                                            <span>${obj.roomsAmount} Rooms</span>
                                                                                                        </div>
                                                                                                        <div class="table-item-area">
                                                                                                            <span>${obj.areaTotal} M &#178;</span>
                                                                                                        </div>
                                                                                                        <div id="${obj.id}" class="table-item-features">


                                                                                                        </div>
                                                                                                    </div>
                                                                                                </a>
                                                                                                `

        tempDiv.innerHTML = apartmentCard;

        //tempDiv.firstChild.firstChild.lastChild.appendChild(container)

        //cosnole.log(tempDiv.firstChild.firstChild.lastChild)

        //var name = obj.number; // Replace 'name' with the actual property you want to display
        //var newElement = document.createElement("div");
        //newElement.style.display = "inline";
        //newElement.style.padding = "10px";
        //newElement.textContent = name;

        dataField.appendChild(tempDiv);

        document.getElementById(obj.id).innerHTML = apartmentFeatures;

    });



    document.getElementById("pagination").innerHTML = "";
    for (var i = 1; i <= Math.ceil(Math.ceil(response.data.total / 10)); i++) {
        var listItem = document.createElement("li");
        listItem.className = "page-item";




        if (i === page) {
            listItem.className += " active";
            listItem.className += " disabled";
        }



        listItem.id = i;

        var link = document.createElement("a");
        link.className = "page-link";
        link.id = i;
        link.innerText = i;

        listItem.appendChild(link);

        // Assuming you have an element with id "pagination" where you want to append the list items
        document.getElementById("pagination").appendChild(listItem);
    }


    var pageLinks = document.querySelectorAll('a.page-link');
    // Add a click event listener to each anchor tag
    pageLinks.forEach(function (link) {
        link.addEventListener('click', function () {
            // Get and log the id attribute of the clicked anchor tag
            var clickedLinkId = link.id;
            page = clickedLinkId;
            updateData().then(function () {

            });

            document.querySelectorAll('li').forEach(function (li) {
                li.classList.remove('disabled');
            });

            // Add the 'disabled' attribute to the li element with id='1'
            var liWithId = document.getElementById(page.toString())
            if (liWithId) {
                liWithId.classList.add('disabled');
            }
        });
    });

}