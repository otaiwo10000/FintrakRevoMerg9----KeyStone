/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAdjustmentCommFeeSearchManualListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IncomeAdjustmentCommFeeSearchManualListController]);

    function IncomeAdjustmentCommFeeSearchManualListController($scope, $state, viewModelHelper, validator)  {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeadjustmentcommfeesearchmanual-list-view';
        vm.viewName = 'Fee And Commission Manual';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.year = 0;
        vm.period = 0;
        vm.search = null;
        vm.cfList = [];

        vm.yearList = [{ value: 2016, name: '2016' },
        { value: 2017, name: '2017' },
        { value: 2018, name: '2018' },
        { value: 2019, name: '2019' },
        { value: 2020, name: '2020' },
        { value: 2021, name: '2021' },
        { value: 2022, name: '2022' },
        { value: 2023, name: '2023' }];

        vm.periodList = [{ value: 1, name: 'January' },
        { value: 2, name: 'Febuary' },
        { value: 3, name: 'Mar' },
        { value: 4, name: 'Apr' },
        { value: 5, name: 'May' },
        { value: 6, name: 'June' },
        { value: 7, name: 'July' },
        { value: 8, name: 'August' },
        { value: 9, name: 'September' },
        { value: 10, name: 'October' },
        { value: 11, name: 'November' },
        { value: 12, name: 'December' }];

        //vm.searchTypes = [
        //    { Id: 0, Name: 'All' },
        //    { Id: 1, Name: 'Caption' },
        //    { Id: 2, Name: 'GLCode' },
        //    { Id: 3, Name: 'Narrative' },
        //    { Id: 4, Name: 'Team' },
        //    { Id: 5, Name: 'AccountOfficer' },
        //    { Id: 6, Name: 'RelatedAccount' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeadjustmentcommfeesearchmanual/availablecommfeemanual/' + vm.year + '/' + vm.period + '/' + vm.search, null,

                    function (result) {
                        vm.cfList = result.data;
                        toastr.success('commfee data loaded.', 'Fintrak');
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error('Fail to load commfee data.', 'Fintrak');
                    }, null);

            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#cfTable').length > 0) {
                    var exportTable = $('#cfTable').DataTable({
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


        vm.reLoad = function () {
            vm.search2a = vm.search;
            vm.search2a = vm.search2a.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.search2a = vm.search2a.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"

            vm.viewModelHelper.apiGet('api/incomeadjustmentcommfeesearchmanual/availablecommfeemanual/' + vm.year + '/' + vm.period + '/' + vm.search2a, null,

                function (result) {
                    vm.cfList = result.data;
                    toastr.success('data loaded.', 'Fintrak');

                },
                function (result) {
                    toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };

        //vm.reLoad = function () {
        //    //vm.viewModelHelper.apiGet('api/mprbalancesheet/balancesheet/' + vm.RunDate.substr(0, 10) , null,
        //    vm.viewModelHelper.apiGet('api/revenue/getrevenue/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number + '/' + vm.RunDate.substr(0, 10) + '/' + vm.ToDate.substr(0, 10), null,
        //        function (result) {
        //            vm.revenue = result.data;
        //        },
        //        function (result) {
        //            toastr.error(result, 'Fintrak Error');
        //        }, null);
        //};


        //initialize(); 
    }
}());
