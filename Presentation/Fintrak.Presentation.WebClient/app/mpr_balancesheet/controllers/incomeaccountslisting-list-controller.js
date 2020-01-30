/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsListingListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                IncomeAccountsListingListController]);

    function IncomeAccountsListingListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'incomeaccountslisting-list-view';
        vm.viewName = 'Income Accounts Listing';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountno = '';
        vm.ial = [];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountslisting/availableincomeaccountslisting', null,
                    function (result) {
                        vm.ial = result.data;
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
                if ($('#ialTable').length > 0) {
                    var exportTable = $('#ialTable').DataTable({
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

        vm.ialLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            //vm.accountno2a = vm.accountno;
            //vm.accountno2a = vm.accountno2a.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            //vm.accountno2a = vm.accountno2a.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"
            vm.ial = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountslisting/getincomeaccountslistingusingaccountnumber/' + vm.searchvalue1, null,
                    function (result) {

                        vm.ial = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshIAL = function () {
            vm.ial = [];
            vm.viewModelHelper.apiGet('api/incomeaccountslisting/availableincomeaccountslisting', null,
                function (result) {

                    vm.ial = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();

    }
}());
