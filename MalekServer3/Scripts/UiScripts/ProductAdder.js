let PropertyList = [];

function btnAddProperty_Click() {
    const property =    document.getElementById('txtProperty').value;
    const value =       document.getElementById('txtValue').value;

    document.getElementById('txtProperty').value = '';
    document.getElementById('txtValue').value = '';

    if (property == '' || property == null) {
        UIkit.notification('مشخصات خالی می باشد');
        return;
    }
    if (value == '' || value == null) {
        UIkit.notification('اطلاعات خالی می باشد');
        return;
    }

    const object = {
        Name: String(property),
        Description: String(value)
    }
    PropertyList.push(object);
    console.log(PropertyList);
    const tBody = document.getElementById('tBody');

    tBody.innerHTML +=
        `
            <tr id="${property}_${value}">
                <td>
                    <a uk-icon="icon: trash; ratio: 1" onclick="btnDeleteProperty_Click('${property}_${value}')"></a>
                </td>
                <td>${property}</td>
                <td>${value}</td>
            </tr>
        `
}

function btnDeleteProperty_Click(trId) {
    const tr = document.getElementById(trId);
    tr.parentNode.removeChild(tr);
    const property = trId.split('_')[0];
    const value = trId.split('_')[1];
    const object = {
        Name: String(property),
        Description: String(value)
    }
    PropertyList.pop(object);
}


let fileData = new FormData();
function btnAddImageClick() {
    const input = document.getElementById('inputImage');
    if (input.value == null || input.value == '')
        return;

    const file = (input.files[0]);



    var fileUpload = $("#inputImage").get(0);
    var files = fileUpload.files;

    // Create FormData object  
    //var fileData = new FormData();

    //debugger;
    // Looping over all files and add it to FormData object  
    for (var i = 0; i < files.length; i++) {
        fileData.append(files[i].name, files[i]);
    }

    //// Adding one more key to FormData object  
    //fileData.append('username', files[0]);

   


    if (!file.type.includes("image")) {
        UIkit.notification('این نوع فایل قابل قبول نمی باشد');
        return;
    }

    if (file) {
        const reader = new FileReader();


        reader.addEventListener("load", function () {
            document.getElementById('addedImages').innerHTML +=
                `
                    <div class="image-item" id="${file.name}">
                        <a class="uk-icon" onclick="RemoveImage('${file.name}')" uk-icon="icon: trash; ratio: 1.4"></a>
                        <img src="${this.result}" alt="" />
                    </div>
                `
        })

        reader.readAsDataURL(file);

        console.log(fileData);
    }
}

function RemoveImage(itemId) {
    const item = document.getElementById(itemId);
    item.parentElement.removeChild(item);
    fileData.delete(itemId);
    console.log(fileData);
}

function ImageAddItToADiv(divId) {
    const input = document.getElementById('inputImage');
    if (input.value == null || input.value == '')
        return;

    const file = (input.files[0]);

    if (!file.type.includes("image")) {
        UIkit.notification('این نوع فایل قابل قبول نمی باشد');
        return;
    }

    if (file) {
        const reader = new FileReader();
        console.log(file);

        reader.addEventListener("load", function () {
            document.getElementById(divId).innerHTML =
                `
                        <img src="${this.result}"/>
                `
        })

        reader.readAsDataURL(file);
    }
}

