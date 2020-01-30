/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BorrowingPeriodicScheduleListController",
                    ['$scope', '$state', '$window', 'viewModelHelper', 'validator',
                        BorrowingPeriodicScheduleListController]);

    function BorrowingPeriodicScheduleListController($scope, $state,$window ,viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'borrowingperiodicschedule-list-view';
        vm.viewName = 'Borrowing Periodic Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.borrowingPeriodicSchedules = [];
        vm.distinctRefNos = [];

        vm.RefNo = 'None';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var initialize = function () {

            if (vm.init === false) {

                intializeLookUp();
                     vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/availableborrowingperiodicschedule' , null,
                    
                   function (result) {
                     //  vm.borrowingPeriodicSchedules = result.data;
                       InitialView();
                       
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);

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
                if ($('#borrowingPeriodicScheduleTable').length > 0) {
                    var exportTable = $('#borrowingPeriodicScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
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
                     toastr.error('Borrowing Periodic Scedule.', 'Fintrak');
                 }, null);
        }
        vm.load = function () {
            var url = '';
            url = 'api/borrowingperiodicschedule/getborrowingperiodicschedule/' + vm.RefNo,

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.borrowingPeriodicSchedules = result.data;

                   toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
               }, null);
        }
        initialize();
        vm.delete = function (RefNo) {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/borrowingperiodicschedule/deleteborrowingperiodicschedule/' + vm.RefNo, null,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-borrowingperiodicschedule-list');
                  vm.RefreshTable();
              },
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
              }, null);
            }
        }
        vm.RefreshTable = function () {
            vm.viewModelHelper.apiGet('api/borrowingperiodicschedule/getborrowingperiodicschedule/' + vm.RefNo, null,
                function (result) {
                    vm.borrowingPeriodicSchedules = result.data;
                },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
        }

            vm.exportToExcel = function (tableId) { 
            $(tableId).tableExport({ type: 'excel', escape: 'false' });
        }

    }
}());
