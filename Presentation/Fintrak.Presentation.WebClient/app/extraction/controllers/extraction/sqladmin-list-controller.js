/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SQLAdminListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        SQLAdminListController]);

    function SQLAdminListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'sqladmin-list-view';
        vm.viewName = 'Extraction and Process Configured';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';

        //var initialize = function () {
        //    InitialView();
        //};

        //var InitialView = function () {
        //    InitialGrid();
        //};

        //var InitialGrid = function () {
        //    setTimeout(function () {

        //        // data export
        //        if ($('#extractionTable').length > 0) {
        //            var exportTable = $('#extractionTable').DataTable({
        //                "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
        //                sDom: "T<'clearfix'>" +
        //                    "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
        //                    "t" +
        //                    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
        //                "tableTools": {
        //                    "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
        //                }
        //            });
        //        }
        //    }, 50);
        //};

        //initialize(); 
    }
}());
