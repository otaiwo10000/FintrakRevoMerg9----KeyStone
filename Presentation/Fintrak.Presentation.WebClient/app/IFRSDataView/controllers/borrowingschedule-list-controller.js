/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BorrowingScheduleListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        BorrowingScheduleListController]);

    function BorrowingScheduleListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'borrowingschedule-list-view';
        vm.viewName = 'Borrowing Schedules';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.borrowingSchedules = [];

        vm.distinctRefNos = [];

        vm.RefNo = 'None';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {

                intializeLookUp();
                vm.init === true;
            }
        }

        var intializeLookUp = function () {
            getRefNos();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#BorrowingScheduleTable').length > 0) {
                    var exportTable = $('#BorrowingScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                             //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 50);
        }

        var getRefNos = function () {
            vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/getrefnos', null,
                 function (result) {
                     vm.distinctRefNos = result.data;
                 },
                 function (result) {
                     toastr.error('Borrowing  Scedule.', 'Fintrak');
                 }, null);
        }
        vm.load = function () {
            var url = '';
            url = 'api/borrowingschedule/getborrowingschedule/' + vm.RefNo,

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.borrowingSchedules = result.data;

                   toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
               }, null);
        }

              vm.exportToExcel = function (tableId) { 
            $(tableId).tableExport({ type: 'excel', escape: 'false' });
        }

        initialize(); 
    }
}());
