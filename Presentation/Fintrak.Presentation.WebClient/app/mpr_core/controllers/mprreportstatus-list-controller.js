/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRReportStatusListController",
        ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        MPRReportStatusListController]);

    function MPRReportStatusListController($scope, $window, $state, viewModelHelper) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprreportstatus-list-view';
        vm.viewName = 'MPR Report Status';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprreportstatus = [];
       
        vm.init = false;
        vm.showInstruction = false;

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/mprreportstatus/availablemprreportstatus', null,
                    function (result) {
                        vm.mprreportstatus = result.data;
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
                if ($('#mprreportstatusTable').length > 0) {
                    var exportTable = $('#mprreportstatusTable').DataTable({
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

        initialize();
    }
}());
