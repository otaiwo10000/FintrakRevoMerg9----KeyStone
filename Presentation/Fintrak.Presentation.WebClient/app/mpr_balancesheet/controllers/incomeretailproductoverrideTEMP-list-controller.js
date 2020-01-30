/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeRetailProductOverrideTEMPListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeRetailProductOverrideTEMPListController]);

    function IncomeRetailProductOverrideTEMPListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomeretailproductoverrideTEMP-list-view';
        vm.viewName = 'Income Retail Product Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.customerid = '';
        vm.bank = '';
        vm.icsprb = [];  

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeretailproductoverrideTEMP/availableincomeretailproductoverride', null,
                    function (result) {
                        vm.icsprb = result.data;
                        InitialView();
                        vm.init === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };


        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#icsprbTable').length > 0) {
                    var exportTable = $('#icsprbTable').DataTable({
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
        };


        vm.icsprbLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeretailproductoverrideTEMP/getincomeretailproductoverrideusingcustomeridandbank/' + vm.customerid + '/' + vm.bank, null,
                    function (result) {

                        vm.icsprb = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshICSPRB = function () {
            vm.icsprb = [];
            vm.viewModelHelper.apiGet('api/incomeretailproductoverrideTEMP/availableincomeretailproductoverride', null,
                function (result) {

                    vm.icsprb = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };



        initialize();

    }
}());
