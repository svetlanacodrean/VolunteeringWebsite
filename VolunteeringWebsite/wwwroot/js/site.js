function showSnack(message) {
    var snack = document.createElement('div');
    snack.classList.add('snackbar');
    snack.innerHTML = message;
    document.body.appendChild(snack);

    snack.classList.add("show");
    setTimeout(function () {
        snack.className = snack.className.replace("show", "");
        document.body.removeChild(snack);
    }, 3000);
}

function createOkWindow(message) {
    var popup = document.createElement('div');
    popup.classList.add('popup-window');

    var p = document.createElement('p');
    p.innerHTML = message;

    var button = document.createElement('button');
    button.classList.add('btn');
    button.classList.add('btn-dark');
    button.innerHTML = 'OK';
    button.onclick = function () {
        document.body.removeChild(popup);
    };

    popup.appendChild(p);
    popup.appendChild(button);

    document.body.appendChild(popup);
}

function createLanguageList(listData, dropDownData) {
    var dataSource = new kendo.data.DataSource({
        pageSize: 20,
        data: listData,
        autoSync: true,
        schema: {
            model: {
                fields: {
                    Language: { defaultValue: { "Id": null, "Name": "select a language" } }
                }
            }
        },
        change: function (e) {
            var dd = dataSource.data().map(function (x) {
                return {
                    "Language": {
                        "Id": x.Language.Id,
                        "Name": x.Language.Name
                    }
                };
            });

            $("#ProjectLanguageList").val(JSON.stringify(dd));
        }
    });

    $("#language-grid").kendoGrid({
        dataSource: dataSource,
        toolbar: [{ name: "create", text: "Add" }],
        columns: [
            { field: "Language", title: "Language", width: "180px", editor: categoryDropDownEditor, template: "#=Language.Name#" },
            { command: { name: "destroy", text: "", width: "10px" }, title: " ", width: "20px" }],
        editable: {
            confirmation: false
        }
    });

    function categoryDropDownEditor(container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataTextField: "Name",
                dataValueField: "Id",
                dataSource: {
                    data: dropDownData
                }
            });
    }
}

function createSkillList(listData, dropDownData) {
    var dataSource = new kendo.data.DataSource({
        pageSize: 20,
        data: listData,
        autoSync: true,
        schema: {
            model: {
                fields: {
                    Skill: { defaultValue: { "Id": null, "Name": "select a skill" } }
                }
            }
        },
        change: function (e) {
            var dd = dataSource.data().map(function (x) {
                return {
                    "Skill": {
                        "Id": x.Skill.Id,
                        "Name": x.Skill.Name
                    }
                };
            });

            $("#ProjectSkillList").val(JSON.stringify(dd));
        }
    });

    $("#skill-grid").kendoGrid({
        dataSource: dataSource,
        toolbar: [{ name: "create", text: "Add" }],
        columns: [
            { field: "Skill", title: "Skill", width: "180px", editor: categoryDropDownEditor, template: "#=Skill.Name#" },
            { command: { name: "destroy", text: "", width: "10px" }, title: " ", width: "20px" }],
        editable: {
            confirmation: false
        }
    });

    function categoryDropDownEditor(container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataTextField: "Name",
                dataValueField: "Id",
                dataSource: {
                    data: dropDownData
                }
            });
    }
}