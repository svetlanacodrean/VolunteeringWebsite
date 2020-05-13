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