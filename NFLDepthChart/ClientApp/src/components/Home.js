import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { playerDetails: [], loading: true };
    }

    componentDidMount() {
        this.fetchDepthChartData();
    }


    static renderDepthChart(playerDetails) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {
                        Object.entries(playerDetails).map(([key,value]) => (
                            <tr key={key}>
                                <td>{key}</td>
                                {
                                    playerDetails[key].map(details => (
                                        <td>#{details.number}, {details.name}</td>

                                ))
                                }
                            </tr>
                        ))}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderDepthChart(this.state.playerDetails);

        return (
            <div>
                <h1 id="tabelLabel" >Depth Chart</h1>
                {contents}
            </div>
        );
    }

    async fetchDepthChartData() {
        const response = await fetch('depthchart');
        const data = await response.json();
        this.setState({ playerDetails: data, loading: false });
    }
}

