import { AddPlayer } from "./components/AddPlayer";
import { RemovePlayer } from "./components/RemovePlayer";
import { GetBackups } from "./components/GetBackups";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/add-player',
        element: <AddPlayer />
    },
    {
        path: '/remove-player',
        element: <RemovePlayer />
    },
    {
        path: '/get-backups',
        element: <GetBackups />
    }
];

export default AppRoutes;
