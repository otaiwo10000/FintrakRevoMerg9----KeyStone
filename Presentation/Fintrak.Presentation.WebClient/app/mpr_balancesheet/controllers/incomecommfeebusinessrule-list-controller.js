/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCommFeeBusinessRuleListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeCommFeeBusinessRuleListController]);

    function IncomeCommFeeBusinessRuleListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecommfeebusinessrule-list-view';
        vm.viewName = 'Income CommFee Business Rule';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.incomeCommFeeBusinessRule = [];

        vm.searchvalue = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomecommfeebusinessrule/availableincomecommfeebusinessrule', null,
                    function (result) {
                        vm.incomeCommFeeBusinessRule = result.data;
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
                if ($('#incomeCommFeeBusinessRuleTable').length > 0) {
                    var exportTable = $('#incomeCommFeeBusinessRuleTable').DataTable({
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

        vm.icflLoad = function () {

            vm.incomeCommFeeBusinessRule = [];
            if (vm.searchvalue === " ") {
                vm.searchvalue = null;
                alert("Search box is empty");
                return;
            };

            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/incomecommfeebusinessrule/getincomecommfeebusinessruleusingsearch/' + vm.searchvalue, null,
                function (result) {

                    vm.incomeCommFeeBusinessRule = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.refreshICFL = function () {
            vm.incomeCommFeeBusinessRule = [];
            vm.viewModelHelper.apiGet('api/incomecommfeebusinessrule/availableincomecommfeebusinessrule', null,
                function (result) {

                    vm.incomeCommFeeBusinessRule = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        initialize(); 
    }
}());
