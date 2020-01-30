/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MisNewOtherListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        MisNewOtherListController]);

    function MisNewOtherListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'misnewothers-list-view';
        vm.viewName = 'Mis New Others';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.misNewOthers = [];
        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.searchvalue1 = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/misnewother/getmisnewother', null,
                   function (result) {
                       vm.misNewOthers = result.data;
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
                if ($('#misnewotherTable').length > 0) {
                    var exportTable = $('#misnewotherTable').DataTable({
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

        //vm.obuLoad = function () {

        //    //vm.icsprb.length = 0;
        //    //this.exportTable.clear();
        //    vm.teamAccessReads = [];
        //    if (vm.init === false) {
        //        vm.viewModelHelper.apiGet('api/teamacessread/getteamaccessreadByParam/' + vm.searchvalue1, null,
        //            function (result) {

        //                vm.teamAccessReads = result.data;
        //            },
        //            function (result) {
        //                toastr.error(result.data, 'Fintrak');
        //            }, null);
        //    }
        //};

        //vm.refreshOBU = function () {
        //    vm.teamAccessReads = [];
        //    vm.viewModelHelper.apiGet('api/teamacessread/getteamaccessread', null,
        //        function (result) {

        //            vm.teamAccessReads = result.data;
        //        },
        //        function (result) {
        //            toastr.error(result.data, 'Fintrak');
        //        }, null);
        //};

        initialize();
    }
}());
