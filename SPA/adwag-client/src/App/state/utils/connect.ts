import { RootState } from '../root.reducer';

export function mapStateToProps(state: RootState) {
	return state;
}

export function mapDispatchToProps(state: any) {
	return state;
}

export type MapStateToPropsType = ReturnType<typeof mapStateToProps>;
export type MapDispatchToPropsType = ReturnType<typeof mapDispatchToProps>;
