import {FileUploader} from "./FileUploader";

const ConnectedUsers = ({ users }) => <div className='user-list'>
    <h4>Connected Users</h4>
    {users.map((u, idx) => <h6 key={idx}>{u}</h6>)}
    <div>
        <FileUploader></FileUploader>
    </div>
</div>

export default ConnectedUsers;