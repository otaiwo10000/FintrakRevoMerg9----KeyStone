/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsTreeAccountListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        IncomeAccountsTreeAccountListController]);

    function IncomeAccountsTreeAccountListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountstreeaccount-list-view';
        vm.viewName = 'Income AccountsTree Account';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountno = '';

        vm.incomeAccountsTreeAccounts = [];
        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountstreeaccount/availableincomeaccountstreeaccount', null,
                   function (result) {
                       vm.incomeAccountsTreeAccounts = result.data;
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
                if ($('#incomeAccountsTreeAccountTable').length > 0) {
                    var exportTable = $('#incomeAccountsTreeAccountTable').DataTable({
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
            vm.accountno2a = vm.accountno;
            vm.accountno2a = vm.accountno2a.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.accountno2a = vm.accountno2a.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountstreeaccount/getincomeaccountstreeaccountusingaccountnumber/' + vm.accountno2a, null,
                    function (result) {

                        vm.incomeAccountsTreeAccounts = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshIAL = function () {
            vm.incomeAccountsTreeAccounts = [];
            vm.viewModelHelper.apiGet('api/incomeaccountstreeaccount/availableincomeaccountstreeaccount', null,
                function (result) {

                    vm.incomeAccountsTreeAccounts = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();
    }
}());
