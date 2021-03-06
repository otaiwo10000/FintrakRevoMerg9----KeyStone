/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAdjustmentVolumeSearchListController",
            ['$scope', '$state', 'viewModelHelper', 'validator',
                IncomeAdjustmentVolumeSearchListController]);

    function IncomeAdjustmentVolumeSearchListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeadjustmentvolumesearch-list-view';
        vm.viewName = 'Details Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.year = 0;
        vm.period = 0;
        vm.search = null;
        vm.searchList = [];
        vm.vsummary = [];

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

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomeadjustmentvolumesearch/incomeadjustmentvolumesearchusingparams/' + vm.year + '/' + vm.period + '/' + vm.search, null,

                    function (result) {
                        vm.vsummary = result.data;
                        toastr.success('sbu summary data loaded.', 'Fintrak');
                        InitialView();
                        vm.init === true;
                    },
                    function (result) {
                        toastr.error('Fail to load sbu summary data.', 'Fintrak');
                    }, null);

            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#vsummaryTable').length > 0) {
                    var exportTable = $('#vsummaryTable').DataTable({
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

        vm.urlFormat = function (n) {
            //vm.search = encodeURI(n);
            vm.search = encodeURIComponent(n);
        }; 

        var downloadBaseCaption = function () {
            vm.viewModelHelper.apiGet('api/downloadbasefintrakfinalCaptiononly/ddbfintrakmanualcaptiononly', null,

                function (result) {
                    vm.searchList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };

        vm.vsummaryLoad = function () {

             //vm.search = encodeURIComponent(vm.search);
            vm.search = vm.search.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.search = vm.search.replace(/\./g, 'DOTXTER');   // i.e replace the xter "." with "FORWARDSLASHXTER"
            //vm.search = vm.search.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
         
            vm.viewModelHelper.apiGet('api/incomeadjustmentvolumesearch/incomeadjustmentvolumesearchusingparams/' + vm.year + '/' + vm.period + '/' + vm.search, null,

                function (result) {
                    vm.vsummary = result.data;
                    toastr.success('data loaded.', 'Fintrak');

                },
                function (result) {
                    toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };


        downloadBaseCaption();
        initialize();
    }
}());
