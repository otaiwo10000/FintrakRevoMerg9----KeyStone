/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AcquirerSharingListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        AcquirerSharingListController]);

    function AcquirerSharingListController($scope, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core ';
        vm.view = 'acquirersharing-list-view';
        vm.viewName = 'Acquirer Sharing';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.acquirersharing = [];  
        //vm.searchvalue = '';

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/acquirersharing/availableacquirersharing', null,
                    function (result) {
                        vm.acquirersharing = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        //vm.acquirermappingLoad = function () {

        //    vm.acquirermapping = [];
        //    if (vm.searchvalue === "") {
        //       alert("Search box is empty");
        //    }

        //    //else if (vm.year === "") {
        //    //    alert("Search with year cannot be empty")
        //    //    return
        //    //}

        //    else {
        //        if (vm.init === false) {
        //            //vm.ts = []; 
        //            vm.viewModelHelper.apiGet('api/acquirermapping/getacquirermappingbysearch/' + vm.searchvalue, null,
        //                function (result) {
                            
        //                    vm.acquirermapping = result.data;
        //                },
        //                function (result) {
        //                    toastr.error(result.data, 'Fintrak');
        //                }, null);
        //        }
        //    }
        //}

       

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#acquirersharingTable').length > 0) {
                    var exportTable = $('#acquirersharingTable').DataTable({
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


        //vm.refreshAM = function () {
        //    vm.acquirermapping = [];
        //    vm.viewModelHelper.apiGet('api/acquirermapping/availableacquirermapping', null,
        //        function (result) {

        //            vm.acquirermapping = result.data;
        //        },
        //        function (result) {
        //            toastr.error(result.data, 'Fintrak');
        //        }, null);
        //};

        initialize();

    }
}());
