/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCWListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        SCWListController]);

    function SCWListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'scorecardweight-list-view';
        vm.viewName = 'MPR ScoreCard Weight';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.scw = [];
        vm.searchvalue = '';
        //vm.year = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/scorecardweight/getscorecardweightwithmetrics', null,
                   function (result) {
                       vm.scw = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        //vm.scwLoad = function () {

        //    vm.scw = [];
        //    //if (vm.searchvalue === "") {
        //    //    alert("Search box cannot be empty");
        //    //}

        //    //else if (vm.year === "") {
        //    //    alert("Search with year cannot be empty")
        //    //    return
        //    //}

        //    //else {
        //        if (vm.init === false) {
        //            //vm.ts = []; 
        //            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
        //            vm.viewModelHelper.apiGet('api/scorecardmetrics/getscorecardmetricsusingsearch/' + vm.searchvalue, null,
        //                function (result) {
                            
        //                    vm.scm = result.data;
        //                },
        //                function (result) {
        //                    toastr.error(result.data, 'Fintrak');
        //                }, null);
        //        }
        //   // }
        //}

       

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#scwTable').length > 0) {
                    var exportTable = $('#scwTable').DataTable({
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

        vm.refreshSCW = function () {
            initialize();
        };

        initialize();
    }
}());
