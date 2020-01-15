﻿using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityModManagerNet
{
    public partial class UnityModManager
    {
        static class Textures
        {
            // Generates via https://www.base64-image.de/ http://angrytools.com/gradient/image/

            private static string WindowBase64 = "iVBORw0KGgoAAAANSUhEUgAAAAIAAAEACAYAAACZCaebAAAAnElEQVRIS63MtQHDQADAwPdEZmaG/fdJCq2g7qqLvu/7hRBCZOF9X0ILz/MQWrjvm1DHdV3MFs7zJLRwHAehhX3fCS1s20ZoYV1XQgvLshDqmOeZ2cI0TYQWxnEktDAMA6GFvu8JLXRdR2ihbVtCHU3TMFuo65rQQlVVhBbKsiS0UBQFoYU8zwktZFlGqCNNU2YLSZIQWojjmFDCH22GtZAncD8TAAAAAElFTkSuQmCC";
            //private static string WindowLighterBase64 = "iVBORw0KGgoAAAANSUhEUgAAABAAAAEACAYAAAC6UvZOAAABbUlEQVR4Xu2RtaEjABTD7OV/mJmZkxnvhlDxGmsAFZJ/v98/ARyBeIPv98suRCDXN/h8PmxjBDJu8H6/2YUI5PoGr9eLbYxAxg2ezye7EIGMGzweD3YhArm+wf1+ZxsjkHGD2+3GLkQg1ze4Xq9sYwQybnC5XNiFCGTc4Hw+swsRyPUNTqcT2xiBjBscj0d2IQK5vsHhcGAbI5Bxg/1+zy5EIOMGu92OXYhArm+w3W7Zxghk3GCz2bALEcj1DdbrNdsYgYwbrFYrdiECGTdYLpfsQgRyfYPFYsE2RiDjBvP5nF2IQK5vMJvN2MYIZNxgOp2yCxHIuMFkMmEXIpDrG4zHY7YxAhk3GI1G7EIEcn2D4XDINkYg4waDwYBdiEDGDfr9PrsQgVzfoNfrsY0RyLhBt9tlFyKQ6xt0Oh22MQIZN2i32+xCBDJu0Gq12IUI5PoGzWaTbYxAxg0ajQa7EIFc3+Dv749tjECmDf4D0bDMnwotAJQAAAAASUVORK5CYII=";
            private static string SettingsNormalBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAQAAABKfvVzAAAACXBIWXMAAA3XAAAN1wFCKJt4AAAJsmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMDlUMTY6MTI6NTArMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTA5VDE4OjMyOjQ2KzAzOjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDE4LTA5LTA5VDE4OjMyOjQ2KzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgcGhvdG9zaG9wOkNvbG9yTW9kZT0iMSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpiZTNlYTRkYy0zYjU4LTIxNDctOWQzMi04NmRiYTFjNmM4MjMiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDoyMmYzNGE4Yy1jZjBiLTVkNDMtODRkYy05ODgyY2UyMTFhYTMiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDpiNjZhYTU5Ny1mODA5LTA0NGYtOWYzYi0wMTZlODdiODFkZjEiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmI2NmFhNTk3LWY4MDktMDQ0Zi05ZjNiLTAxNmU4N2I4MWRmMSIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxNjoxMjo1MCswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY29udmVydGVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJmcm9tIGltYWdlL3BuZyB0byBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDoyNGZjMTdlMS1jOWY3LTk5NDUtYmFkOC00NWZhNGI1MjgwNTgiIHN0RXZ0OndoZW49IjIwMTgtMDktMDlUMTg6MzI6MjcrMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmZiYTVjMTY5LTkzNGItOGI0NS1hMjg3LTc2NzJkYjc1MjY2ZiIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxODozMjo0NiswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY29udmVydGVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJkZXJpdmVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJjb252ZXJ0ZWQgZnJvbSBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIHRvIGltYWdlL3BuZyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6YmUzZWE0ZGMtM2I1OC0yMTQ3LTlkMzItODZkYmExYzZjODIzIiBzdEV2dDp3aGVuPSIyMDE4LTA5LTA5VDE4OjMyOjQ2KzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDwvcmRmOlNlcT4gPC94bXBNTTpIaXN0b3J5PiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpmYmE1YzE2OS05MzRiLThiNDUtYTI4Ny03NjcyZGI3NTI2NmYiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6YjY2YWE1OTctZjgwOS0wNDRmLTlmM2ItMDE2ZTg3YjgxZGYxIiBzdFJlZjpvcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6YjY2YWE1OTctZjgwOS0wNDRmLTlmM2ItMDE2ZTg3YjgxZGYxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+KBN97QAAAGtJREFUOI3FkTESgCAMBDcOhX6ZHr+snZZy4MRBUbcLmYS7nCWAkYMVIWpJAGDJXgyXwW/XWNraBgIG5EOFpNJDs6RwttWj+YebkhTnBN/l4EjqlIPipvJ+Dl08CHN2s2h/Bac8lXRpmknLHSshDz5/7DVxAAAAAElFTkSuQmCC";
            private static string SettingsActiveBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAQAAABKfvVzAAAACXBIWXMAAA3XAAAN1wFCKJt4AAAJsmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMDlUMTY6MTI6NTArMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTA5VDE4OjMzOjAyKzAzOjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDE4LTA5LTA5VDE4OjMzOjAyKzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgcGhvdG9zaG9wOkNvbG9yTW9kZT0iMSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDowNTVkNWY0My00MmViLTdiNDMtOTdkZi1mYjc2MWY2NzcyNjkiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDoyZjNlNWU4NC04NzM4LTdlNDEtYmExZi00MzljMWU3YjI2NTEiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDpiNjZhYTU5Ny1mODA5LTA0NGYtOWYzYi0wMTZlODdiODFkZjEiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmI2NmFhNTk3LWY4MDktMDQ0Zi05ZjNiLTAxNmU4N2I4MWRmMSIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxNjoxMjo1MCswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY29udmVydGVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJmcm9tIGltYWdlL3BuZyB0byBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDoyNGZjMTdlMS1jOWY3LTk5NDUtYmFkOC00NWZhNGI1MjgwNTgiIHN0RXZ0OndoZW49IjIwMTgtMDktMDlUMTg6MzI6MjcrMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjQ1NGEwNjIxLTQwZmMtNTU0Yy1hZDkzLWNkM2ZjMzIxMTdlYyIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxODozMzowMiswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY29udmVydGVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJkZXJpdmVkIiBzdEV2dDpwYXJhbWV0ZXJzPSJjb252ZXJ0ZWQgZnJvbSBhcHBsaWNhdGlvbi92bmQuYWRvYmUucGhvdG9zaG9wIHRvIGltYWdlL3BuZyIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6MDU1ZDVmNDMtNDJlYi03YjQzLTk3ZGYtZmI3NjFmNjc3MjY5IiBzdEV2dDp3aGVuPSIyMDE4LTA5LTA5VDE4OjMzOjAyKzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDwvcmRmOlNlcT4gPC94bXBNTTpIaXN0b3J5PiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo0NTRhMDYyMS00MGZjLTU1NGMtYWQ5My1jZDNmYzMyMTE3ZWMiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6YjY2YWE1OTctZjgwOS0wNDRmLTlmM2ItMDE2ZTg3YjgxZGYxIiBzdFJlZjpvcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6YjY2YWE1OTctZjgwOS0wNDRmLTlmM2ItMDE2ZTg3YjgxZGYxIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+rc0l4AAAAF5JREFUOI3FkUsSgCAMQx+OC73/Zd3VpUYhTMcPb0chk9CUAFg42LCUAIjzxAsmf11ziP4jFcDXkZKCtENaMFdmdgV/9aC8Hek+api1I1nGFKdcdjamOOVppP6nVz3uep4WJ6dlbD4AAAAASUVORK5CYII=";
            private static string StatusActiveBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAIf2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMDlUMTc6MDg6NTIrMDM6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMDlUMTc6MDk6MDErMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTA5VDE3OjA5OjAxKzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDoyOWQ1NWMzNi0xNzYxLTE1NDYtOTgyMC1kMWRkNjliZDE0NTciIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDplOTEwMGY5Yi00NTU4LWE2NDYtYTY3Ny0xY2I0NjY3YTRjYjciIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDplODZhMjVmNi1lNmU0LTdlNDEtYWI0MC02MzZkMzljZDkwZjgiIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmU4NmEyNWY2LWU2ZTQtN2U0MS1hYjQwLTYzNmQzOWNkOTBmOCIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxNzowODo1MiswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6NWYwNzJhODUtYjU4Zi1mMzQ4LTliOGQtZGQyZjE3NDQyOGY2IiBzdEV2dDp3aGVuPSIyMDE4LTA5LTA5VDE3OjA5OjAxKzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjb252ZXJ0ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImZyb20gYXBwbGljYXRpb24vdm5kLmFkb2JlLnBob3Rvc2hvcCB0byBpbWFnZS9wbmciLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImRlcml2ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImNvbnZlcnRlZCBmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDoyOWQ1NWMzNi0xNzYxLTE1NDYtOTgy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";
            private static string StatusInactiveBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAIf2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMDlUMTc6MDg6NTIrMDM6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMDlUMTc6NTg6MzErMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTA5VDE3OjU4OjMxKzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDphYjJiZWI3Ni0wY2JiLWIyNDctYjdhYS0zMWI1NjYzYjJjNDEiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDpiNzRkODNkZC03NmM0LWQ1NDYtYWEyNi02N2Y3YjMyMGNlYzgiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDplODZhMjVmNi1lNmU0LTdlNDEtYWI0MC02MzZkMzljZDkwZjgiIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmU4NmEyNWY2LWU2ZTQtN2U0MS1hYjQwLTYzNmQzOWNkOTBmOCIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxNzowODo1MiswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6ZThjZTMxMGEtODRkYi1lNjQyLWEyYWYtOWQyNDQ0OWUwOTBkIiBzdEV2dDp3aGVuPSIyMDE4LTA5LTA5VDE3OjU4OjMxKzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjb252ZXJ0ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImZyb20gYXBwbGljYXRpb24vdm5kLmFkb2JlLnBob3Rvc2hvcCB0byBpbWFnZS9wbmciLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImRlcml2ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImNvbnZlcnRlZCBmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDphYjJiZWI3Ni0wY2JiLWIyNDctYjdhYS0zMWI1NjYzYjJjNDEiIHN0RXZ0OndoZW49IjIwMTgtMDktMDlUMTc6NTg6MzErMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPC9yZGY6U2VxPiA8L3htcE1NOkhpc3Rvcnk+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOmU4Y2UzMTBhLTg0ZGItZTY0Mi1hMmFmLTlkMjQ0NDllMDkwZCIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDplODZhMjVmNi1lNmU0LTdlNDEtYWI0MC02MzZkMzljZDkwZjgiIHN0UmVmOm9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDplODZhMjVmNi1lNmU0LTdlNDEtYWI0MC02MzZkMzljZDkwZjgiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6CJz7aAAAD3UlEQVRIiaWWS0grVxzGf2dmjIYQm/iKWRhEr6UQrQXRhXTVLHqr9QVxobhxewUXQttr6baPS6GL0nZ53VUkGxeF3m677sJ2fKT4SNCSaywSIYnJvDJdmBkmqba39MDHOcOc833/1/nPiK2tLWRZRlGUhlmSJGZmZsLAEvAe8CbQC+jAH8AJ8COwDRR4YChCCCRJQgjhYm5uLgR8CGzec8YHvFHH+8C3wOfAl/cJKUIIAJd8YWHhSf0QAOfn51xfX1MqlTAMAyEEra2t+P1+Ojs7icVi1A3ZBNaA7xoEnIVt2ySTyY+BTwHOzs7IZrNomtbgIYBhGBSLRS4vLzk+Pqa/v5/BwUHqhoWAzxxeeX5+HiEEy8vLq8DXAPv7+6TTaUzTdMWd2UGtVsO2bXRdJ5/PY5omPT09AAngHNgDkGq1GisrK68BzwFUVSWdTmNZ1n9COp1GVVXH8Od1T+4EgKcAR0dHqKqKaZqYpollWe7aMAx37YV3j6qqHB4eOiIfAQjbtjuAa4BUKkWlUkGSpAZ44+/NmRMqL/x+P4uLi862ToW7Oufg4ICbm5sHyR8SaBbRNI2DgwPi8TjAkgK8C5DJZNB1/X8L1Go1MpmMI/BYAd4yTZNcLtcgcN8FfEigWSSXy2GaJoqijCpApFgsUigUqNVqf7P+VXLQLKDrOsVikXA4HFEArVwu+wzDcG9qM7kkSQCuiHMvvOTeWZIkSqUS4XAYBXhZrVaDQggqlUpDSP6tipqJHQQCAarVKkBeAX63bfv1lpYWNE3Dtu0GkWbcF38vALq6uhw7flWAnwKBwEwoFOL09NTd5ITkVZPsPRMKhQgEAgAvFGC7u7v7m1gsxt7eHuVyuSHO95E3i3jJA4EAsViM7u5ugG356uqqMjs722aa5tsXFxdu42puA95nb9twepGTh4GBASYnJ4lEIl8APyh1C54NDQ09jcfjFAoFstks9R7l5uQhD5x3kiTR399PPB5naGgI4BmAZNs2q6urN7IsP0kkEsTjcaLRaIOlTgk3w7snGo0yPDxMIpFAluU14AZAHh8fx7IsUqnUL0tLS9bg4OA7hUKB29tbyuUyhmH8Y9X4fD76+voYGxsjmUzS3t7+CfCV46U8MTGBbdtYlsXOzs7PKysrf46MjEx3dHQghGi4VJZlAeDz+QgGg/T29jI6OsrU1BTT09P4/f41LzmAWF9fR9M0F4ZhkEqlwsAH1Wp1U1VVTk5OyOfzFItFAILBIJFIhEePHjEyMkJbW9uDH32xsbHhkuu67sKyLHZ3d8PAMvCYu9+WaP3cS+A34AXw/X3EzvgLFKrw+QQN6BEAAAAASUVORK5CYII=";
            private static string StatusNeedRestartBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAIf2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0RXZ0PSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VFdmVudCMiIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMTgtMDktMDlUMTc6MDg6NTIrMDM6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMDlUMTc6NTc6MTArMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDE4LTA5LTA5VDE3OjU3OjEwKzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDphYjljYTE2MS01NTA1LTM2NDItYjBmZi04NWM2YTQ4OTQzOTMiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDpkZjAzYzgxNS01ZGFhLTZjNDUtOWU4ZC03NWJhYWViNWY0OGEiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDplODZhMjVmNi1lNmU0LTdlNDEtYWI0MC02MzZkMzljZDkwZjgiIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOmU4NmEyNWY2LWU2ZTQtN2U0MS1hYjQwLTYzNmQzOWNkOTBmOCIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0wOVQxNzowODo1MiswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6MGE1N2VhNDUtMjc3ZC01MjQxLWE2MjktYjlmYTFhYTNlNTk0IiBzdEV2dDp3aGVuPSIyMDE4LTA5LTA5VDE3OjU3OjEwKzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiBzdEV2dDpjaGFuZ2VkPSIvIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjb252ZXJ0ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImZyb20gYXBwbGljYXRpb24vdm5kLmFkb2JlLnBob3Rvc2hvcCB0byBpbWFnZS9wbmciLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImRlcml2ZWQiIHN0RXZ0OnBhcmFtZXRlcnM9ImNvbnZlcnRlZCBmcm9tIGFwcGxpY2F0aW9uL3ZuZC5hZG9iZS5waG90b3Nob3AgdG8gaW1hZ2UvcG5nIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDphYjljYTE2MS01NTA1LTM2NDItYjBm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";
            private static string WWWBase64 = "iVBORw0KGgoAAAANSUhEUgAAAB8AAAAfCAYAAAAfrhY5AAAACXBIWXMAAA7EAAAOxAGVKw4bAAAGjWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiB4bXA6Q3JlYXRlRGF0ZT0iMjAxOC0wOS0xOFQyMDowMjo0OSswMzowMCIgeG1wOk1vZGlmeURhdGU9IjIwMTgtMDktMTlUMTI6MjE6MDkrMDM6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMTlUMTI6MjE6MDkrMDM6MDAiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjllZWQwYzk5LWQ4YmItMTk0Yi1hOTU2LWI0Mzg4MzZiNGE1NyIgeG1wTU06RG9jdW1lbnRJRD0iYWRvYmU6ZG9jaWQ6cGhvdG9zaG9wOjE0YmE2MTBjLTcxMzMtM2E0Yy05NDkyLWUwNTczZDE5YmUxOCIgeG1wTU06T3JpZ2luYWxEb2N1bWVudElEPSJ4bXAuZGlkOjRlYjUxNDViLTY5YjEtODE0Zi1hNmEyLWRhNGU3ZDliMjlmMyI+IDx4bXBNTTpIaXN0b3J5PiA8cmRmOlNlcT4gPHJkZjpsaSBzdEV2dDphY3Rpb249ImNyZWF0ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6NGViNTE0NWItNjliMS04MTRmLWE2YTItZGE0ZTdkOWIyOWYzIiBzdEV2dDp3aGVuPSIyMDE4LTA5LTE4VDIwOjAyOjQ5KzAzOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDozYWU1NzdiOS00MWEwLTU1NGUtYTU1NC0zMmViZmI1ZDFjNzMiIHN0RXZ0OndoZW49IjIwMTgtMDktMThUMjA6MDY6MTErMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPHJkZjpsaSBzdEV2dDphY3Rpb249InNhdmVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjllZWQwYzk5LWQ4YmItMTk0Yi1hOTU2LWI0Mzg4MzZiNGE1NyIgc3RFdnQ6d2hlbj0iMjAxOC0wOS0xOVQxMjoyMTowOSswMzowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz5HbzubAAAEM0lEQVRIicWXTWwVVRTHf2doQUKnMWkhgOK7o2gMoiCGHZBoQozs+LCyMOwq8T2iW1pJCCZQjStiZxpwZ4wp5WslCxdScFUaJMpHjBrmloABeWxmXiFt6RwXnfcYXqcfiTY9izfz7v3N+Z+cuffMuaKqiAgzWp8uKJSH3xC0U9DVCi2itKjwQOAByh+JOF8M3VvyK4ckmcmdqiKzEff86DOEdpDCzFHqEHA8LDYfmUncmVIwiHZ6fvS3F8SKyDpN2INqr6JXaWR5WHQFICy6kiSyQtGrqPZqwh5V2eAFsXpBdKfQE22fSiNX3PhxF8pJRFag2h8W3TYHeQ6R3Q1jC7aF7e69LD+0r+luw9iCbYjsRmSFLbm7gAsgK52E08aPD89KfE2fLhRhvwqjoMWw6L6zpk8XqnAE4K9Pl9zOc1QdF6HrrWPaGH7c9DawT2FMhM41fbpwWnEviL56VK6MoKyzxeZnwmJzDyL66H7lCoIR1Z35CaxZG+A9GI8vI6Jh0fVtqXmRiqx/VK6MGD/uysK1BVcIKu856LkZnP83U1DkXVtq+vGpBSckx+dUeEIESL55EowqAOnq/AnVSfsunZuUci+INWfs/bxxVMX48cXqnKpO/JggOmCC+GResF5PvNX40WDuXJ4IYPxo0OuJt+bPxae8IOqsic+H1SrcfFmDtfYMsJ2J5dAGnAB2p9cy0JLOAZwBNgHLsqwx5oS1tgxcNMbsALDWKnDfGLPMWlvPKnDWAQZSx8+mjgG2pKItdcEOAEvrWWttlb1Uxy+11uaxAJccoLqYXgE2p/ebgVdzMjUTWy8+LSthGAJcAx4CG4HvgA+B31PwOrA240zTIDYC3wJ7qqwxprZNrbXXgNemYMeNMWurRWYgBQD602s1wrxtVmXP17FZG5iGHYQntT2brvM8bQNMbf3TzNW/giw7kCd+C7gJ2AyYW2BS1taxU4nfMsZk2UFgfvf5vFY4ByZ6NOPHp/KguarttQiqjrwg7p/Tr1oQ/5z9qtWaCeNHQyLyQl60/7OFYdF9sa57ddrnfAEoIOx98j+jZ/y4ywtiLXRX1mefMX501Qti9bqjXdnx+vQaP/7AC2I1QfRbdrwQVN70glizPdykvt2W3I7FrU2LHEevGD8a8YK4hKq0NLgbUCyO5DYcVROhF+Vm4Z67IX3Hnxg/GnXQXxa3Ni2yJbcjy09qnW+0yajCYYFGoNvrqfRf3itjidABsPro8PN5wi/5D1dNRMD+C4fksempXBQ4KtAAfH6jTUbrn8k9NNiieyCBHajeAbaYID6NcBfV3seN4+cK3ZXlWX7V18Mrx+XxD6r6faKUvSA+K7BJ0dsJ7AiL7sHcTM3yrNYB8hGCmRacsFBVj9lS85fTQbM+KAJwUB2zfPh10aQDeFmVVoFWhbIIZeBPVI6E/zRdm+0p9V+lFDjVQcCm0AAAAABJRU5ErkJggg==";
            private static string UpdatesBase64 = "iVBORw0KGgoAAAANSUhEUgAAAEIAAABCCAYAAADjVADoAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAFwmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiB4bXA6Q3JlYXRlRGF0ZT0iMjAxOC0wOS0xOFQyMDozNjo0NSswMzowMCIgeG1wOk1vZGlmeURhdGU9IjIwMTgtMDktMTlUMTE6MDY6NTkrMDM6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMTgtMDktMTlUMTE6MDY6NTkrMDM6MDAiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOmE1Zjg4NDg3LWU0MmUtMDc0OS04MDYwLWE5YmMwNWM2Yzg2NCIgeG1wTU06RG9jdW1lbnRJRD0iYWRv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";

            public static Texture2D Window = null;
            //public static Texture2D WindowLighter = null;
            public static Texture2D SettingsNormal = null;
            public static Texture2D SettingsActive = null;
            public static Texture2D StatusActive = null;
            public static Texture2D StatusInactive = null;
            public static Texture2D StatusNeedRestart = null;
            public static Texture2D WWW = null;
            public static Texture2D Updates = null;

            public static void Init()
            {
                var stringFields = typeof(Textures).GetFields(BindingFlags.Static | BindingFlags.NonPublic).Where(x => x.FieldType == typeof(string)).ToArray();
                var textureFields = typeof(Textures).GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => x.FieldType == typeof(Texture2D)).ToArray();
                foreach (var f in textureFields)
                {
                    f.SetValue(null, new Texture2D(2, 2, TextureFormat.ARGB32, false, true));
                }

                if (unityVersion.Major >= 2017)
                {
                    var assembly = Assembly.Load("UnityEngine.ImageConversionModule");
                    var LoadImage = assembly.GetType("UnityEngine.ImageConversion").GetMethod("LoadImage", new Type[] { typeof(Texture2D), typeof(byte[]) });
                    if (LoadImage != null)
                    {
                        foreach (var f in textureFields)
                        {
                            LoadImage.Invoke(null, new object[] { (Texture2D)f.GetValue(null), Convert.FromBase64String((string)stringFields.FirstOrDefault(x => x.Name == f.Name + "Base64")?.GetValue(null) ?? "") });
                        }
                    }
                }
                else
                {
                    var LoadImage = typeof(Texture2D).GetMethod("LoadImage", new Type[] { typeof(byte[]) } );
                    if (LoadImage != null)
                    {
                        foreach (var f in textureFields)
                        {
                            LoadImage.Invoke((Texture2D)f.GetValue(null), new object[] { Convert.FromBase64String((string)stringFields.FirstOrDefault(x => x.Name == f.Name + "Base64")?.GetValue(null) ?? "") });
                        }
                    }
                }

                int resize = 128;
                SettingsNormal.ResizeToIfLess(resize);
                SettingsActive.ResizeToIfLess(resize);
                WWW.ResizeToIfLess(resize);
                Updates.ResizeToIfLess(resize);
            }
        }
    }
}