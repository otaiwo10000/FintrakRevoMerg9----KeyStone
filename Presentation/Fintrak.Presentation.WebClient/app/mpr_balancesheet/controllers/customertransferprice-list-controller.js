/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CustomerTransferPriceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        CustomerTransferPriceListController]);

    function CustomerTransferPriceListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'customertransferprice-list-view';
        vm.viewName = 'Customer Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.custtp = [];  
        vm.searchvalue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/customertransferprice/getcustomertransferpricebysetup', null,
                    function (result) {
                        vm.custtp = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        vm.custtpLoad = function () {

            vm.custtp = [];
            if (vm.searchvalue === "") {
               alert("Search box is empty");
            }

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            else {
                if (vm.init === false) {
                    //vm.ts = []; 
                    vm.viewModelHelper.apiGet('api/customertransferprice/getcustomertransferpricebysearch/' + vm.searchvalue, null,
                        function (result) {
                            
                            vm.custtp = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        }

       

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#custtpTable').length > 0) {
                    var exportTable = $('#custtpTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        "searching": false,
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


        vm.refreshCUSTTP = function () {
            vm.custtp = [];
            vm.viewModelHelper.apiGet('api/customertransferprice/getcustomertransferpricebysetup', null,
                function (result) {

                    vm.custtp = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();

    }
}());
