(function(){
    addContactButtonListener();
    addCloseButtonListener();
    addInsertButtonListener();
    addUpdateButtonListener();
    addSubmitFormListener();
    addDeleteButtonListener();
    addSubmitDeleteButtonListener();
}())

function addContactButtonListener(){
    $('.contact-button').click(function(event){
        let supplierId = $(this).attr('data-id');
        $.ajax({
            url: `/api/supplier/contact/${supplierId}`,
            success: function({address, city, phone, email}){
                $('.contact-dialog .address').text(address);
                $('.contact-dialog .city').text(city);
                $('.contact-dialog .phone').text(phone);
                $('.contact-dialog .email').text(email);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.contact-dialog').addClass('popup-dialog--opened');
            }
        });
    });
}

function addCloseButtonListener(){
    $('.close-button').click(function(event){
        $('.modal-layer').removeClass('modal-layer--opened');
        $('.popup-dialog').removeClass('popup-dialog--opened');
        $('.popup-dialog input').val("");
        $('.popup-dialog textarea').val("");
        $('.popup-dialog .validation-message').text("");
    });
}

function addInsertButtonListener(){
    $('.create-button').click(function(event){
        event.preventDefault();
        $('.modal-layer').addClass('modal-layer--opened');
        $('.form-dialog').addClass('popup-dialog--opened');
    });
}

function addUpdateButtonListener(){
    $('.update-button').click(function(event){
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        $.ajax({
            url: `/api/supplier/${supplierId}`,
            success: function(response){
                populateInputForm(response);
                $('.modal-layer').addClass('modal-layer--opened');
                $('.form-dialog').addClass('popup-dialog--opened');
            }
        })
    });
}

function populateInputForm({id, companyName, contactPerson, jobTitle, address, city, phone, email}){
    $('.form-dialog .id').val(id);
    $('.form-dialog .companyName').val(companyName);
    $('.form-dialog .contactPerson').val(contactPerson);
    $('.form-dialog .jobTitle').val(jobTitle);
    $('.form-dialog .address').val(address);
    $('.form-dialog .city').val(city);
    $('.form-dialog .phone').val(phone);
    $('.form-dialog .email').val(email);
}

function addSubmitFormListener(){
    $('.form-dialog button').click(function(event){
        event.preventDefault();
        let dto = collectInputForm();
        let requestMethod = (dto.id === null) ? 'POST' : 'PUT';
        $.ajax({
            method: requestMethod,
            url: '/api/supplier',
            data: JSON.stringify(dto),
            contentType: 'application/json',
            success: function(response){
                location.reload();
            },
            error: function({status, responseJSON}){
                if(status === 422){
                    writeValidationMessage(responseJSON);
                }
            }
        });
    })
}

function collectInputForm(){
    let id = $('.form-dialog .id').val()
    let dto = {
        id: (id === "") ? null : id,
        companyName: $('.form-dialog .companyName').val(),
        contactPerson: $('.form-dialog .contactPerson').val(),
        jobTitle: $('.form-dialog .jobTitle').val(),
        address: $('.form-dialog .address').val(),
        city: $('.form-dialog .city').val(),
        phone: $('.form-dialog .phone').val(),
        email: $('.form-dialog .email').val()
    };
    return dto;
}

function writeValidationMessage(errorMessages){
    for(let error of errorMessages){
        let {field, message} = error;
        $(`.form-dialog [data-for=${field}]`).text(message);
    }
}

function addDeleteButtonListener(){
    $('.delete-button').click(function(event){
        event.preventDefault();
        let supplierId = $(this).attr('data-id');
        $('.delete-dialog .id').val(supplierId);
        $('.modal-layer').addClass('modal-layer--opened');
        $('.delete-dialog').addClass('popup-dialog--opened');
    });
}

function addSubmitDeleteButtonListener(){
    $('.delete-dialog button').click(function(event){
        let supplierId = $('.delete-dialog .id').val();
        $.ajax({
            method: "DELETE",
            url: `/api/supplier/${supplierId}`,
            success: function(response){
                location.reload();
            },
            error: function(response){
                alert(`Ada error dengan status code: ${response.status}`);
            }
        });
    });
}