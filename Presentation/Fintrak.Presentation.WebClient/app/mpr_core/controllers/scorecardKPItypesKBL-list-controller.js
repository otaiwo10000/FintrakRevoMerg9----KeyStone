/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardKPITypesKBLListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ScoreCardKPITypesKBLListController]);

    function ScoreCardKPITypesKBLListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'scorecardKPItypesKBL-list-view';
        vm.viewName = 'ScoreCard KPI Types';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.kpi = [];
        vm.searchvalueList = [{ name: 'n1', value: 'v1' }, { name: 'n2', value: 'v2' }, { name: 'n3', value: 'v3' }];
        vm.searchvalue = null;
        vm.year = null;
        vm.period = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getallscorecardKPItypesKBL', null,
                    function (result) {
                        vm.kpi = result.data;
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
                if ($('#kpiTable').length > 0) {
                    var exportTable = $('#kpiTable').DataTable({
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
        };

        vm.kpiLoad = function () {

            vm.kpi = [];
            if (vm.searchvalue === "") {
                alert("Search box is empty");
                return;
            }

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            //else {
            //    if (vm.init === false) {
            //vm.ts = []; 
            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            //vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getscorecardKPItypesKBLusingsearch/' + vm.searchvalue, null,
            vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getscorecardKPItypesKBLusingperiodyearsearchvalue/' + vm.period + '/' + vm.year + '/' + vm.searchvalue, null,

            function (result) {

                    vm.kpi = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
            // }
            // }
        };

        vm.refreshKPI = function () {
            vm.kpi = [];
            vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getallscorecardKPItypesKBL', null,
                function (result) {

                    vm.kpi = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
        
    }
}());
