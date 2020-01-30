/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardSetMetricTargetKBLListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        ScoreCardSetMetricTargetKBLListController]);

    function ScoreCardSetMetricTargetKBLListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'scorecardsetmetrictargetKBL-list-view';
        vm.viewName = 'ScoreCard Set Metric Target';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mtarget = [];
        vm.searchvalue = '';
        vm.period = null;
        vm.year = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/scorecardsetmetrictargetKBL/getallscorecardsetmetrictargetKBL', null,
                    function (result) {
                        vm.mtarget = result.data;
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
                if ($('#mtargetTable').length > 0) {
                    var exportTable = $('#mtargetTable').DataTable({
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

        vm.mtargetLoad = function () {

            vm.mtarget = [];
            //if (vm.searchvalue === "") {
            //    alert("Search box is empty");
            //    return;
            //}

            //else if (vm.year === "") {
            //    alert("Search with year cannot be empty")
            //    return
            //}

            //else {
            //    if (vm.init === false) {
            //vm.ts = []; 
            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            //vm.viewModelHelper.apiGet('api/scorecardsetmetrictargetKBL/getscorecardsetmetrictargetKBLusingsearch/' + vm.searchvalue, null,
            vm.viewModelHelper.apiGet('api/scorecardsetmetrictargetKBL/getscorecardsetmetrictargetKBLusingperiodANDyear/' + vm.period + '/' + vm.year, null,
                function (result) {

                    vm.mtarget = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
            // }
            // }
        };

        vm.refreshMTARGET = function () {
            vm.mtarget = [];
            vm.viewModelHelper.apiGet('api/scorecardsetmetrictargetKBL/getallscorecardsetmetrictargetKBL', null,
                function (result) {

                    vm.mtarget = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        initialize();
        
    }
}());
