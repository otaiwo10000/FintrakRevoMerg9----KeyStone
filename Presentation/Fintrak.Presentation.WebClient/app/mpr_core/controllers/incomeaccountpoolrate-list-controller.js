/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountPoolRateListController",
            ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                IncomeAccountPoolRateListController]);

    function IncomeAccountPoolRateListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountpoolrate-list-view';
        vm.viewName = 'Income Account PoolRate';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountPoolRates = [];

        vm.searchvalue = null;

        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeaccountpoolrate/availableincomeaccountpoolrate', null,
                    function (result) {
                        vm.incomeAccountPoolRates = result.data;
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
                if ($('#incomeAccountPoolRateTable').length > 0) {
                    var exportTable = $('#incomeAccountPoolRateTable').DataTable({
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

        vm.icflLoad = function () {

            vm.incomeCustomerPoolRates = [];
            if (vm.searchvalue == " ") {
                vm.searchvalue = null;
                alert("Search box is empty");
                return
            }

            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/incomeaccountpoolrate/getincomeaccountpoolrateusingsearch/' + vm.searchvalue, null,
                function (result) {

                    vm.incomeAccountPoolRates = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.refreshICFL = function () {
            vm.incomeAccountPoolRates = [];
            vm.viewModelHelper.apiGet('api/incomeaccountpoolrate/availableincomeaccountpoolrate', null,
                function (result) {

                    vm.incomeAccountPoolRates = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize();
    }
}());
