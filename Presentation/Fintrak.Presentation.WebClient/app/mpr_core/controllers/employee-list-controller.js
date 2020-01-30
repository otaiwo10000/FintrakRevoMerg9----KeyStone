/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AbcRatioListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        AbcRatioListController]);

    function AbcRatioListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

     
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        $scope.selectedFile = null;
        $scope.msg = "";  

        $scope.loadFile = function (files) {
            $scope.$apply(function () {
                $scope.selectedFile = files[0];
            });
        };

        $scope.handleFile = function () {
            var file = $scope.selectedFile;
            if (file) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var data = e.target.result;
                    var workbook = XLSX.read(data, { type: 'binary' });
                    var first_sheet_name = workbook.SheetNames[0];
                    var dataObjects = XLSX.utils.sheet_to_json(workbook.Sheets[first_sheet_name]);
                    //console.log(excelData);  
                    if (dataObjects.length > 0) {
                        $scope.save(dataObjects);
                    } else {
                        $scope.msg = "Error : Something Wrong !";
                    }
                };
                reader.onerror = function (ex) {

                };
                reader.readAsBinaryString(file);
            };
        };


        $scope.save = function (data) {
            $http({
                method: "POST",
                url: "api/Employee/Save",
                data: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (data) {
                if (data.status) {
                    $scope.msg = "Data has been inserted ! ";
                }
                else {
                    $scope.msg = "Error : Something Wrong";
                }
            }, function (error) {
                $scope.msg = "Error : Something Wrong";
            });
        }; 



        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/abcratio/availableabcratio', null,
                   function (result) {
                       vm.abcRatios = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#abcRatioTable').length > 0) {
                    var exportTable = $('#abcRatioTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        initialize();
    }
}());
