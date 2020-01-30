/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductTransferPriceListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ProductTransferPriceListController]);

    function ProductTransferPriceListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'producttransferprice-list-view';
        vm.viewName = 'Product Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ptp = [];
        vm.searchvalue = '';
        //vm.year = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/producttransferprice/getallproducttransferprice', null,
                   function (result) {
                       vm.ptp = result.data;
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
                if ($('#ptpTable').length > 0) {
                    var exportTable = $('#ptpTable').DataTable({
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

        vm.ptpLoad = function () {

            vm.ptp = [];
            if (vm.searchvalue == "") {
                alert("Search box is empty");
                return
            }

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            //else {
            //    if (vm.init === false) {
            //vm.ts = []; 
            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/producttransferprice/getproducttransferpriceusingsearch/' + vm.searchvalue, null,
                function (result) {

                    vm.ptp = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
            // }
            // }
        }

        vm.refreshPTP = function () {
            vm.ptp = [];
            vm.viewModelHelper.apiGet('api/producttransferprice/getallproducttransferprice', null,
                function (result) {

                    vm.ptp = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
        
    }
}());
