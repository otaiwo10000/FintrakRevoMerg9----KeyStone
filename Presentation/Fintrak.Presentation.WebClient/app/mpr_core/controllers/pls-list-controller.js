/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLsListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        PLsListController]);

    function PLsListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'pls-list-view';
        vm.viewName = 'PL';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeincomenewdetails = [];
        vm.incomeincomeotherbreakdown = [];

        vm.searchvalue1 = '';
        vm.searchvalue2 = '';

        vm.period = 1;
        vm.period2 = 1;

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

        vm.periodList2 = [{ value: 1, name: 'January' },
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

        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.init2 = false;
        vm.showInstruction = false;
        vm.instruction = '';

        //vm.searchvalue1 = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pls/getincomeincomenewdetails', null,
                   function (result) {
                       vm.incomeincomenewdetails = result.data;
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
                if ($('#iindTable').length > 0) {
                    var exportTable = $('#iindTable').DataTable({
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

        vm.obuLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.incomeincomenewdetails = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pls/getincomeincomenewdetailsByParam/' + vm.searchvalue1 + '/' + vm.period, null,
                    function (result) {

                        vm.incomeincomenewdetails = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU = function () {
            vm.incomeincomenewdetails = [];
            vm.viewModelHelper.apiGet('api/pls/getincomeincomenewdetails', null,
                function (result) {

                    vm.incomeincomenewdetails = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        //======================================================================================
        //======================================================================================

        var initialize2 = function () {

            if (vm.init2 === false) {
                vm.viewModelHelper.apiGet('api/pls/getincomeincomeotherbreakdown', null,
                    function (result) {
                        vm.incomeincomeotherbreakdown = result.data;
                        InitialView2();
                        vm.init2 === true;

                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        var InitialView2 = function () {
            InitialGrid2();
        }

        var InitialGrid2 = function () {
            setTimeout(function () {

                // data export
                if ($('#iiobTable').length > 0) {
                    var exportTable = $('#iiobTable').DataTable({
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

        vm.obuLoad2 = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.incomeincomeotherbreakdown = [];
            if (vm.init2 === false) {
                vm.viewModelHelper.apiGet('api/pls/getincomeincomeotherbreakdownByParam/' + vm.searchvalue2 + '/' + vm.period2, null,
                    function (result) {

                        vm.incomeincomeotherbreakdown = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU2 = function () {
            vm.incomeincomeotherbreakdown = [];
            vm.viewModelHelper.apiGet('api/pls/getincomeincomeotherbreakdown', null,
                function (result) {

                    vm.incomeincomeotherbreakdown = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        //======================================================================

        vm.tToggle = false;
        vm.toggleText = "collapse New Details list";
        var toggleTextFunc = function () {
            if (vm.tToggle === true) {
                vm.toggleText = "show New Details list";
            }
            else if (vm.tToggle === false) {
                vm.toggleText = "collapse New Details list";
            }
        };

        vm.toggleReportPage = function () {
            vm.tToggle = !vm.tToggle;
            document.getElementById("newdetail").hidden = vm.tToggle;
            toggleTextFunc();
        };

        //========================================================================

        vm.tToggle2 = false;
        vm.toggleText2 = "collapse Other Breakdown list";
        var toggleTextFunc2 = function () {
            if (vm.tToggle2 === true) {
                vm.toggleText2 = "show Other Breakdown list";
            }
            else if (vm.tToggle2 === false) {
                vm.toggleText2 = "collapse Breakdown list";
            }
        };

        vm.toggleReportPage2 = function () {
            vm.tToggle2 = !vm.tToggle2;
            document.getElementById("otherbreakdown").hidden = vm.tToggle2;
            toggleTextFunc2();
        };

        //=======================================================================

        vm.toggleReportPage();
        vm.toggleReportPage2();

        initialize();
        initialize2();
    }
}());
