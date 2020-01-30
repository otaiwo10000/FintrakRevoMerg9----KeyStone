/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexRawExpensesFixedListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        OpexRawExpensesFixedListController]);

    function OpexRawExpensesFixedListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'mpr-Opex';
        vm.view = 'opexrawexpensesfixed-list-view'; 
        vm.viewName = 'Opex Raw Expenses Fixed';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexRawExpensesFixed = [];
        //vm.opexStaffExpDiff = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/opexrawexpensesfixed/availableopexrawexpensesfixed', null,
                    function (result) {
                        vm.opexRawExpensesFixed = result.data;
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
                if ($('#opexRawExpenseTable').length > 0) {
                    var exportTable = $('#opexRawExpenseTable').DataTable({
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

        vm.SearchValue = '';
        vm.opexLoad = function () {

            vm.opexRawExpensesFixed = [];
            if (vm.SearchValue === "") {
                alert("Search with team name or team code cannot be empty");
                return;
            }

            else {
                if (vm.init === false) {
                    //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                    vm.viewModelHelper.apiGet('api/opexrawexpensesfixed/availableopexrawexpensesfixed_2/' + vm.SearchValue, null,
                        function (result) {
                            vm.opexRawExpensesFixed = result.data;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
            }
        };
       
        vm.refresh = function () {
            vm.opexRawExpensesFixed = [];
            if (vm.init === false) {
                //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
                vm.viewModelHelper.apiGet('api/opexrawexpensesfixed/availableopexrawexpensesfixed', null,
                    function (result) {
                        vm.opexRawExpensesFixed = result.data;
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.downloadtoexcel = function () {
            vm.viewModelHelper.apiGet('api/filedownload/ExcelFileDownLoadOnClient', null,
                    function (result) {
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
        };



        initialize();
    }
}());
